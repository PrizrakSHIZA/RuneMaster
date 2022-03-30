using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{
    public Unit stats;

    public Image hpBar;

    public int currentHP;

    public int currentSquare;
    public bool playerControl;

    const float _TimeToMove = 1f;

    private void Start()
    {
        currentHP = stats.hp;
    }

    public void Move()
    {
        //change owner modifier which will be used in bext formulas
        int owner = -1;
        if (playerControl)
            owner = 1;


        //Check for unit on the way
        for (int i = 1; i <= stats.speed; i++)
        {
            UnitController unit = Gameplay.Singleton.squares[currentSquare + i * owner].unitOn;
            if (unit != null)
            {
                //check if ally
                if (unit.IsAlly(this))
                {
                    int moveTo = unit.currentSquare - 1 * owner;
                    PerformMove(moveTo, true);
                    return;
                }
                else
                {
                    int moveTo = unit.currentSquare - 1 * owner;
                    PerformMove(moveTo);
                    return;
                }

            }
        }

        //If noone on the way
        int to = currentSquare + stats.speed * owner;
        PerformMove(to, false);
    }

    void PerformMove(int moveTo, bool attack = false)
    {
        Gameplay.Singleton.squares[currentSquare].unitOn = null;
        if (moveTo >= Gameplay.Singleton.squares.Count)
        {
            moveTo = Gameplay.Singleton.squares.Count - 2;
            attack = true;
        }
        else if (moveTo <= 0)
        {
            moveTo = 1;
            attack = true;
        }

        int diff = Mathf.Abs(currentSquare - moveTo);
        GetComponent<RectTransform>().DOAnchorPos(Gameplay.Singleton.squares[moveTo].anchoredPosition, diff * _TimeToMove)
            .OnComplete(() => 
            {
                if (attack)
                    Attack(); //do nothing
            });
        currentSquare = moveTo;
        Gameplay.Singleton.squares[currentSquare].unitOn = this;
    }

    public void Attack()
    {
        Debug.Log("Attack!");
        if (playerControl)
        {
            if (currentSquare == Gameplay.Singleton.squares.Count - 2)
            {
                //Enemy controller
                Debug.Log("Attack enemy hero!");
            }
            else
            {
                Gameplay.Singleton.squares[currentSquare + 1].unitOn.TakeDamage(stats.damage);
            }
        }
        else
        {
            if (currentSquare == 1)
            {
                PlayerController.Singleton.TakeDamage(stats.damage);
                Debug.Log("Attack player hero!");
            }
            else
            {
                Gameplay.Singleton.squares[currentSquare - 1].unitOn.TakeDamage(stats.damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, stats.hp);
        Debug.Log($"Under attack! {currentHP}");

        hpBar.DOFillAmount((100f / stats.hp * currentHP) / 100f, 2f).OnComplete(()=>
        {
            if (currentHP == 0)
            {
                if (playerControl)
                    Gameplay.Singleton.playerUnits.Remove(this);
                else
                    Gameplay.Singleton.enemyUnits.Remove(this);
                Destroy(gameObject);
            }
        });
   }

    public bool IsAlly(UnitController unit)
    {
        if (unit.playerControl && playerControl || !unit.playerControl && !playerControl) 
            return true;
        else 
            return false;
    }

    public void GetDamageModifier()
    {

    }
}

