using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Singleton;

    [SerializeField] Button endTurn;
    [SerializeField] GameObject prefab;

    public Transform field;

    public List<Square> squares;

    public List<UnitController> playerUnits = new List<UnitController>();
    public List<UnitController> enemyUnits = new List<UnitController>();

    [SerializeField] ParticleSystem PS;

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
        PlayerController.Singleton.RegenMana();
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
            temp.GetComponent<RectTransform>().anchoredPosition = squares[0].anchoredPosition;
            unitControler.currentSquare = 0;
            playerUnits.Add(unitControler);
        }
        else
        {
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
}
