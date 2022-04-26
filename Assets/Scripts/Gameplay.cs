using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Singleton;

    [SerializeField] Button endTurn;
    [SerializeField] GameObject prefab;
    [Header("GameOver settings")]
    [SerializeField] GameObject gameoverScreen;
    [SerializeField] Text text;

    public Transform field;

    public List<Square> squares;

    public List<UnitController> playerUnits = new List<UnitController>();
    public List<UnitController> enemyUnits = new List<UnitController>();

    // -----------------
    private void Start()
    {
        Singleton = this;
    }

    public void EndTurn()
    {
        StartCoroutine(EndTurnCoro());
    }

    IEnumerator EndTurnCoro()
    {
        endTurn.interactable = false;
        PlayerController.Singleton.CanCast(false);
        yield return new WaitUntil(() => UnitController.inAction == 0);
        AIController.Singleton.RegenMana();
        MoveUnits(enemyUnits);
        yield return new WaitUntil(() => UnitController.inAction == 0);
        AIController.Singleton.ChooseAction();
    }

    public void StartTurn()
    {
        StartCoroutine(StartTurnCoro());
    }

    IEnumerator StartTurnCoro()
    {
        yield return new WaitUntil(() => UnitController.inAction == 0);
        PlayerController.Singleton.RegenMana();
        PlayerController.Singleton.SpellChanged(null, null);
        MoveUnits(playerUnits);
        yield return new WaitUntil(() => UnitController.inAction == 0);
        endTurn.interactable = true;
        PlayerController.Singleton.CanCast();
    }

    void MoveUnits(List<UnitController> list)
    {
        foreach (UnitController unit in list)
        {
            unit.Move();
        }
    }

    public void Summon(Unit unit, bool player)
    {
        var temp = Instantiate(prefab, field);
        var unitControler = temp.GetComponent<UnitController>();

        unitControler.stats = unit;
        unitControler.playerControl = player;
        if (player)
        {
            if (squares[0].unitOn != null)
                squares[0].unitOn.TakeDamage(999);
            temp.GetComponent<RectTransform>().anchoredPosition = squares[0].anchoredPosition;
            unitControler.currentSquare = 0;
            playerUnits.Add(unitControler);
        }
        else
        {
            if (squares[squares.Count - 1].unitOn != null)
                squares[squares.Count - 1].unitOn.TakeDamage(999);
            temp.GetComponent<RectTransform>().anchoredPosition = squares[squares.Count - 1].anchoredPosition;
            unitControler.currentSquare = squares.Count - 1;
            enemyUnits.Add(unitControler);
        }
        squares[unitControler.currentSquare].unitOn = unitControler;
    }

    public void SpawnParticle(string name, int target)
    {
        var temp = Instantiate(Resources.Load<ParticleSystem>($"ParticleSystem/{name}"), squares[target].transform);
        temp.transform.position += new Vector3(0, 1.5f, 0);
        Destroy(temp, 5f);
    }

    //Searching methods for AI
    public int GetNearestEnemySquare()
    {
        int target = -1;
        for (int i = 1; i < squares.Count; i++)
        {
            if (squares[i].unitOn != null && squares[i].unitOn.playerControl)
            {
                target = i;
            }
        }
        return target;
    }

    public int GetLowestEnenmySquare()
    {
        int target = -1;
        UnitController unit = new UnitController();
        unit.currentHP = int.MaxValue;
        for (int i = 1; i < squares.Count; i++)
        {
            if (squares[i].unitOn != null && squares[i].unitOn.playerControl)
            {
                if (squares[i].unitOn.currentHP < unit.currentHP)
                { 
                    unit = squares[i].unitOn;
                    target = i;
                }
            }
        }
        return target;
    }


    public void Win()
    {
        gameoverScreen.SetActive(true);
        text.text = "You win!";
        text.color = Color.green;
    }

    public void Lose()
    {
        gameoverScreen.SetActive(true);
        text.text = "You lose!";
        text.color = Color.red;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
