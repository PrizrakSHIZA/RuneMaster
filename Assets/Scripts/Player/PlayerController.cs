using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    public List<Runes> currentSpell = new List<Runes>();

    public PlayerData data;

    [SerializeField] Button castBtn;

    int mana;
    int hp;

    private void Start()
    {
        Singleton = this;
        data = SaveSystem.LoadData();
        if (data == null)
        {
            data = new PlayerData();
        }

        hp = data.maxHP;
        mana = data.maxMana;
    }

    public void Cast()
    {
        //Safety mode
        if (currentSpell.Count == 0)
            return;

        foreach (Spell spell in Spell.Spells.Values)
        {
            if (currentSpell.SequenceEqual(spell.spellRunes))
            {
                if (spell is SpellSummon)
                    (spell as SpellSummon).Cast(true);
                else if (spell is SpellTarget)
                {
                    throw new KeyNotFoundException();

                    int target = 0;
                    //Choose target, then
                    (spell as SpellTarget).Cast(true, target);
                }
                Casted();
                return;
            }
        }
        Casted();
        Debug.Log("No such spell");
    }

    public void Casted()
    {
        currentSpell.Clear();
        RuneBook.Singleton.ClearRunes();
        castBtn.interactable = false;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            //GameOver
        }
        Debug.Log($"Now hp is {hp}");
    }

    public void CanCast()
    {
        castBtn.interactable = true;
    }
}
