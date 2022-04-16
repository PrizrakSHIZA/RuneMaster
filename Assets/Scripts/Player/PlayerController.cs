using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    public ObservableCollection<Runes> currentSpell = new ObservableCollection<Runes>();

    public PlayerData data;

    [SerializeField] Button castBtn;
    [Space()]
    [Header("Mana")]
    [SerializeField] Image manaFill;
    [SerializeField] Image manaUsage;

    public Color usingColor = new Color(29, 80, 168, 255);
    public Color redColor = new Color(168, 29, 39, 255);

    int mana;
    int hp;

    //----------------------------------
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

        currentSpell.CollectionChanged += SpellChanged;
    }

    void SpellChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        manaUsage.fillAmount = 0;
        foreach (Runes rune in currentSpell)
        {
            manaUsage.fillAmount += (float)Rune.GetRune(rune).cost / (float)data.maxMana;
            if (!EnoughtMana())
                manaUsage.color = redColor;
            else
                manaUsage.color = usingColor;
        }
    }

    public void Cast()
    {
        //Safety mode
        if (currentSpell.Count == 0)
            return;

        if (!TrySpendMana())
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

    //Mana
    public void RegenMana()
    {
        mana += data.manaRegen;
        UpdateManaUI();
    }
    
    private bool TrySpendMana()
    {
        int manacost = 0;
        foreach (Runes rune in currentSpell)
        {
            manacost += Rune.GetRune(rune).cost;
        }
        if (manacost > mana)
            return false; // not enought mana
        else
        {
            mana -= manacost;
            UpdateManaUI();
            return true;
        }
    }

    private bool EnoughtMana()
    {
        int manacost = 0;
        foreach (Runes rune in currentSpell)
        {
            manacost += Rune.GetRune(rune).cost;
        }
        if (manacost > mana)
            return false; // not enought mana
        else
        {
            return true;
        }
    }

    private void UpdateManaUI()
    {
        manaFill.fillAmount = (float)mana / (float)data.maxMana;
    }
}
