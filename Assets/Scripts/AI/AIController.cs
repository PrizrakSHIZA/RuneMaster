using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    public static AIController Singleton;

    public AI_SO AI;

    public int hp;

    [Header("HP")]
    [SerializeField] Image hpBar;

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
                (spell as SpellSummon).Cast(false);
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

        hpBar.DOFillAmount((100f / AI.HP * hp) / 100f, 1f);

        if (hp <= 0)
        {
            hp = 0;
            //Win
        }
        Debug.Log($"Now hp is {hp}");
    }
}
