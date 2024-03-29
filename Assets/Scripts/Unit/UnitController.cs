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

    public static int inAction;

    private int Owner
    {
        get { if (playerControl) return 1; else return -1; }
    }

    private void Start()
    {
        currentHP = stats.hp;
        GetComponent<Image>().sprite = stats.sprite;
        if (playerControl)
            GetComponent<RectTransform>().localScale = new Vector3(-GetComponent<RectTransform>().localScale.x, GetComponent<RectTransform>().localScale.y, 1f);
    }

    public void Move()
    {
        inAction++;

        //Check if can attack
        for (int i = 1; i <= stats.range; i++)
        {
            if (currentSquare + i * Owner >= Gameplay.Singleton.squares.Count - 1 || currentSquare + i * Owner < 0)
                break;

            UnitController unit = Gameplay.Singleton.squares[currentSquare + i * Owner].unitOn;
            if (unit != null)
            {
                PerformMove(currentSquare, !unit.IsAlly(this));
                return;
            }
        }

        //Check for unit on the way
        for (int i = 1; i <= stats.speed + stats.range; i++)
        {
            if (currentSquare + i * Owner >= Gameplay.Singleton.squares.Count-1 || currentSquare + i * Owner < 0)
                break;

            UnitController unit = Gameplay.Singleton.squares[currentSquare + i * Owner].unitOn;
            if (unit != null)
            {
                //check if ally
                int moveTo = unit.currentSquare - stats.range * Owner;
                PerformMove(moveTo, !unit.IsAlly(this));
                return;
            }
        }

        //If noone on the way
        int to = currentSquare + stats.speed * Owner;
        bool attack = false;
        if (to >= Gameplay.Singleton.squares.Count - 1 && playerControl)
        {
            to = Gameplay.Singleton.squares.Count - 2;
            attack = true;
        }
        else if (to <= 0 && Owner == -1)
        {
            to = 1;
            attack = true;
        }
        PerformMove(to, attack);
    }

    void PerformMove(int moveTo, bool attack = false)
    {
        Gameplay.Singleton.squares[currentSquare].unitOn = null;
        int diff = Mathf.Abs(currentSquare - moveTo);
        currentSquare = moveTo;
        Gameplay.Singleton.squares[currentSquare].unitOn = this;

        GetComponent<RectTransform>().DOAnchorPos(Gameplay.Singleton.squares[moveTo].anchoredPosition, diff * _TimeToMove)
            .OnComplete(() => 
            {
                if (attack)
                    Attack();
                else
                    inAction--;
            });
    }

    void Attack()
    {
        if (playerControl)
        {
            if (currentSquare == Gameplay.Singleton.squares.Count - 2)
            {
                //Attack Enemy controller
                AIController.Singleton.TakeDamage(stats.damage);
                AttackAnimation();
            }
            else
            {
                //Attack enemy unit
                Gameplay.Singleton.squares[currentSquare + stats.range].unitOn.TakeDamage(stats.damage, stats.attackType);
                AttackAnimation();
            }
        }
        else
        {
            if (currentSquare == 1)
            {
                //Attack player controller
                AttackAnimation();
                PlayerController.Singleton.TakeDamage(stats.damage);
            }
            else
            {
                //Attack player unit
                Gameplay.Singleton.squares[currentSquare - stats.range].unitOn.TakeDamage(stats.damage, stats.attackType);
                AttackAnimation();
            }
        }
    }

    public void TakeDamage(int damage, DamageType attackType)
    {
        damage = Mathf.RoundToInt(damage * Unit.GetDamageModifier(attackType, stats.armorType));
        inAction++;
        currentHP = Mathf.Clamp(currentHP - damage, 0, stats.hp);
        Debug.Log($"Under attack! {currentHP}");
        TakeDamageAnimation(); // take 1s right now. Shold be checked that its faster than next animation (2s now)

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
            inAction--;
        });
   }

    public bool IsAlly(UnitController unit)
    {
        if (unit.playerControl && playerControl || !unit.playerControl && !playerControl) 
            return true;
        else 
            return false;
    }

    void AttackAnimation()
    {
        int owner = -1;
        if (playerControl)
            owner = 1;
        Vector3 startPos = transform.position;
        Vector3 endPos = (startPos + Gameplay.Singleton.squares[currentSquare + 1 * owner].transform.position);
        endPos = new Vector3(endPos.x / 2, startPos.y, startPos.z);

        transform.DOMove(endPos, 0.3f).OnComplete(() => 
        {
            transform.DOMove(startPos, 0.5f).OnComplete(() => 
            {
                inAction--;
            });
        });
    }

    public void TakeDamageAnimation()
    {
        transform.DOShakePosition(1f, 3);
        GetComponent<Image>().DOColor(Color.red, 0.5f).OnComplete(() =>
        {
            if(currentHP != 0)
                GetComponent<Image>().DOColor(Color.white, 0.5f);
        });
    }
}

