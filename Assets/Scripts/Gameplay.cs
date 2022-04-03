using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Singleton;

    [SerializeField] Button endTurn;

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
        MoveUnits(enemyUnits);
        AIController.Singleton.ChooseAction();
        endTurn.interactable = false;
    }

    public void StartTurn()
    {
        MoveUnits(playerUnits);
        playerTurn = true;
        endTurn.interactable = true;
    }

    void MoveUnits(List<UnitController> list)
    {
        foreach (UnitController unit in list)
        {
            unit.Move();
        }
    }
}
