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
    public int mana;
    [Header("HP")]
    [SerializeField] Image hpBar;

    private void Start()
    {
        Singleton = this;
        hp = AI.HP;
        mana = AI.mana;
    }

    public void ChooseAction()
    {
        if (Random.Range(1, 11) <= AI.smartChance)
        {
            throw new KeyNotFoundException();
        }
        else
        {
            int tries = 0;
            Spell spell = null;
            //Make N tries to cast some spell
            while (tries != -1)
            {
                Spell.Spells.TryGetValue(AI.spellList[Random.Range(0, AI.spellList.Count)], out spell);
                if (!EnoughtMana(spell))
                    tries++;
                else
                { 
                    tries = -1;
                    Debug.Log("AI casted" + spell.ToString());
                }    
                if (tries >= 2)
                {
                    Debug.Log("AI passes");
                    Gameplay.Singleton.StartTurn();
                    return;
                }
            }

            if (spell is SpellSummon)
            {
                (spell as SpellSummon).Cast(false);
                SpendMana(spell);
            }
            else if (spell is SpellTarget)
            {
                int nearest = Gameplay.Singleton.GetNearestEnemySquare();
                if (nearest == -1)
                {
                    ChooseAction();
                    return;
                }
                if (nearest >= 8)
                {
                    (spell as SpellTarget).Cast(false, nearest);
                    SpendMana(spell);
                }
                else
                { 
                    (spell as SpellTarget).Cast(false, Gameplay.Singleton.GetLowestEnenmySquare());
                    SpendMana(spell);
                }
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
            Gameplay.Singleton.Win();
        }
        Debug.Log($"Now hp is {hp}");
    }

    public void RegenMana()
    {
        mana += AI.manaRegen;
    }

    private bool EnoughtMana(Spell currentSpell)
    {
        int manacost = 0;
        for (int i = 0; i < currentSpell.spellRunes.Count; i++)
        {
            manacost += Rune.GetRune(currentSpell.spellRunes[i]).cost;
        }
        if (manacost > mana)
            return false; // not enought mana
        else
        {
            return true;
        }
    }

    private void SpendMana(Spell currentSpell)
    {
        int manacost = 0;
        for (int i = 0; i < currentSpell.spellRunes.Count; i++)
        {
            manacost += Rune.GetRune(currentSpell.spellRunes[i]).cost;
        }
        mana -= manacost;
    }
}
