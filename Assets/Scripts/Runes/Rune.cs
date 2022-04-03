using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune
{
    public string name { get; }
    public Runes type { get; }
    public Thome thome { get; }
    public Sprite sprite { get; }

    Rune(string name, Runes type, Thome thome = Thome.general)
    {
        this.name = name;
        this.type = type;
        this.thome = thome;
        sprite = Resources.Load<Sprite>($"Runes/{name}");
    }

    public static List<Rune> RuneList = new List<Rune>
    {
        new Rune("Igni", Runes.igni),
        new Rune("Hito", Runes.hito),
        new Rune("Magnus", Runes.magnus),
        new Rune("Kojo", Runes.kojo),
        new Rune("Lutum", Runes.lutum),
        new Rune("Celer", Runes.celer),
        new Rune("Yami", Runes.yami),
    };

    public static Rune GetRune(Runes type)
    {
        foreach (Rune rune in RuneList)
        {
            if (rune.type == type)
                return rune;
        }
        Debug.LogError("No such rune found!");
        throw new NotImplementedException(); 
    }
}

public enum Runes
{ 
    empty,
    igni,
    hito,
    magnus,
    kojo,
    lutum,
    celer,
    yami,
    NumberOf
}

public enum Thome
{ 
    general,
    beasts,
    elementals,
    necronomicon,
    NumberOf
}
