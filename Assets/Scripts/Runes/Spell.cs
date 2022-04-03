using System;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public List<Runes> spellRunes = new List<Runes>();

    public Spell(Runes rune1 = Runes.empty, Runes rune2 = Runes.empty, Runes rune3 = Runes.empty)
    {
        if (rune1 != Runes.empty)
            spellRunes.Add(rune1);
        if (rune2 != Runes.empty)
            spellRunes.Add(rune2);
        if (rune3 != Runes.empty)
            spellRunes.Add(rune3);
    }

    public static Dictionary<Spells, Spell> Spells = new Dictionary<Spells, Spell>()
    {
        { global::Spells.S_Soldier, new SpellSummon(Runes.test1, Runes.test2, Cast: (object sender) => {

        })},

        { global::Spells.S_BigSoldier, new SpellSummon(Runes.test1, Runes.test2, Runes.test3, (object sender) =>
        {
            Debug.Log("Spell2");
        })},
    };
}

public enum Spells
{ 
    S_Soldier,
    S_BigSoldier,
}

public class SpellSummon : Spell
{
    public Action<object> Cast;

    public SpellSummon(Runes rune1 = Runes.empty, Runes rune2 = Runes.empty, Runes rune3 = Runes.empty, Action<object> Cast = null) : base(rune1, rune2 , rune3)
    { 
        this.Cast = Cast;
    }
}

public class SpellTarget : Spell
{
    public Action<object, int> Cast;

    public SpellTarget(Runes rune1 = Runes.empty, Runes rune2 = Runes.empty, Runes rune3 = Runes.empty, Action<object, int> Cast = null) : base(rune1, rune2, rune3)
    {
        this.Cast = Cast;
    }
}