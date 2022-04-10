using System;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
    public string name { get; }
    public Runes type { get; }
    public Thome thome { get; }
    public Sprite sprite { get; }

    public int cost { get; }

    Rune(string name, Runes type, Thome thome = Thome.general, int cost = 1)
    {
        this.name = name;
        this.type = type;
        this.thome = thome;
        this.cost = cost;
        sprite = Resources.Load<Sprite>($"Runes/{name}");
    }

    public static List<Rune> RuneList = new List<Rune>
    {
        new Rune("Igni", Runes.igni, cost:5),
        new Rune("Hito", Runes.hito, cost:5),
        new Rune("Magnus", Runes.magnus, cost:3),
        new Rune("Kojo", Runes.kojo, cost:5),
        new Rune("Lutum", Runes.lutum, cost:5),
        new Rune("Celer", Runes.celer, cost:2),
        new Rune("Yami", Runes.yami, cost:5),
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
