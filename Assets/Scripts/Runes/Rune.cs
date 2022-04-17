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
    public string description { get; }

    Rune(string name, Runes type, Thome thome = Thome.general, int cost = 1, string description = "No description yet")
    {
        this.name = name;
        this.type = type;
        this.thome = thome;
        this.cost = cost;
        sprite = Resources.Load<Sprite>($"Runes/{name}");
        this.description = description;
    }

    public static List<Rune> RuneList = new List<Rune>
    {
        /* <b><i>yami</i></b> */
        new Rune("Igni", Runes.igni, cost:5, description:"���������� <b><i>igni</i></b>\n��� ��������� <b><i>igni</i></b>...\n<b><i>igni</i></b> �������� ��� �������"),
        new Rune("Hito", Runes.hito, cost:4, description:"<i>magnus</i> <b><i>hito</i></b> ����� ������ �������\n����� �������� �������� � ���� - <b><i>hito</i></b>"),
        new Rune("Magnus", Runes.magnus, cost:3, description:"<b><i>magnus</i></b> <i>igni</i> ������ �������� ���.\n�� ��� <b><i>magnus</i></b> <i>hito</i>\n������ <b><i>magnus</i></b> ���� ����� ������������ <b><i>magnus</i></b> ����"),
        new Rune("Kojo", Runes.kojo, cost:4, description:"...� �������� �� ����� ������� � ���������� <b><i>kojo</i></b>\n���� ��������� <b><i>kojo</i></b> ��������, �� ��, ��� �������� ������� ��� ������ ������ ���"),
        new Rune("Lutum", Runes.lutum, cost:3, description:"��������� ������� ������� �� <b><i>lutum</i></b> � ���, ��� <b><i>lutum</i></b> ����� ����� ����� ����� ��� ������.\n������� �� ������ ��� ������ <b><i>lutum</i></b> ��� ���������� ��������"),
        new Rune("Celer", Runes.celer, cost:2, description:"����� ������, ���� ���� �� ������ <b><i>celer</i></b> � ���������, �� � ������.\n...���������� �������� �������� ������� ��� �������, ���� ����������� <b><i>celer</i></b> � <i>magnus</i>"),
        new Rune("Yami", Runes.yami, cost:6, description:"������� <b><i>yami</i></b> ��� ������ ���...\n�������� <b><i>yami</i></b> ����� ������ ���������, �� ������ ��� ���� �� ����.\n���� �� ������� ���� �� ��������� ��������� <b><i>yami</i></b>"),
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
