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

    Rune(string name, Runes type, Thome thome)
    {
        this.name = name;
        this.type = type;
        this.thome = thome;
        sprite = Resources.Load<Sprite>($"Runes/{name}");
    }

    public static List<Rune> RuneList = new List<Rune>
    {
        new Rune("Test1", Runes.test1, Thome.general),
        new Rune("Test2", Runes.test2, Thome.general),
        new Rune("Test3", Runes.test3, Thome.general),
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
    test1,
    test2,
    test3,
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
