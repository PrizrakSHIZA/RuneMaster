using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController Singleton;

    public List<Spell> spellList;

    [Range(0,10)]
    public int smartChance;

    int maxHP;
    int hp;

    private void Start()
    {
        Singleton = this;   
    }

    public void ChooseAction()
    {
        if (Random.Range(1, 11) <= smartChance)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            Spell spell = spellList[Random.Range(0, spellList.Count)];
            if (spell is SpellSummon)
            {
                (spell as SpellSummon).Cast(this);
            }
            else if (spell is SpellTarget)
            {
                throw new KeyNotFoundException();
            }
        }
        Gameplay.Singleton.StartTurn();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            //Win
        }
        Debug.Log($"Now hp is {hp}");
    }
}
