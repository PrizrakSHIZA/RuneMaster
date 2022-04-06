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

    bool playerTurn = true;

    private void Start()
    {
        Singleton = this;
    }

    public void EndTurn()
    {
        playerTurn = false;
        endTurn.interactable = false;
        MoveUnits(enemyUnits);
        AIController.Singleton.ChooseAction();
    }

    public void StartTurn()
    {
        MoveUnits(playerUnits);
        playerTurn = true;
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
}
