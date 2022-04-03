using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController Singleton;

    public AI_SO AI;

    public int hp;

    private void Start()
    {
        Singleton = this;
        hp = AI.HP;
    }

    public void ChooseAction()
    {
        if (Random.Range(1, 11) <= AI.smartChance)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            Spell spell; 
            Spell.Spells.TryGetValue(AI.spellList[Random.Range(0, AI.spellList.Count)], out spell);
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
