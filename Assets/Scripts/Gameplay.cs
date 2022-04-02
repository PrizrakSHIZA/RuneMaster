using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Singleton;

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
        /*
        if (!playerTurn)
        {
            Debug.Log("Not player turn!");
            return;
        }*/
        playerTurn = false;
        MoveUnits(enemyUnits);
        AIController.Singleton.ChooseAction();
    }

    public void StartTurn()
    {
        Debug.Log("Player turn!");
        MoveUnits(playerUnits);
        playerTurn = true;
    }

    void MoveUnits(List<UnitController> list)
    {
        foreach (UnitController unit in list)
        {
            unit.Move();
        }
    }
}
