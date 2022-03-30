using System;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public List<Runes> spellRunes = new List<Runes>();

    public Action Cast;

    public Spell(Runes rune1 = Runes.empty, Runes rune2 = Runes.empty, Runes rune3 = Runes.empty, Action Cast = null)
    {
        if (rune1 != Runes.empty)
            spellRunes.Add(rune1);
        if (rune2 != Runes.empty)
            spellRunes.Add(rune2);
        if (rune3 != Runes.empty)
            spellRunes.Add(rune3);

        this.Cast = Cast;
    }

    public static List<Spell> Spells = new List<Spell>()
    {
        new Spell(Runes.test1, Runes.test2, Cast: () => 
        { 
            Debug.Log("Spell1"); 
        }),
        new Spell(Runes.test1, Runes.test2, Runes.test3, () => 
        { 
            Debug.Log("Spell2"); 
        }),
    };
}
