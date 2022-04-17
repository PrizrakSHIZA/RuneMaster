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
        new Rune("Igni", Runes.igni, cost:5, description:"Обжигающий <b><i>igni</i></b>\nОни расжигали <b><i>igni</i></b>...\n<b><i>igni</i></b> поглотил его целиком"),
        new Rune("Hito", Runes.hito, cost:4, description:"<i>magnus</i> <b><i>hito</i></b> стоит тысячи обычных\nСамое страшное животное в мире - <b><i>hito</i></b>"),
        new Rune("Magnus", Runes.magnus, cost:3, description:"<b><i>magnus</i></b> <i>igni</i> кольцо освещало все.\nОн был <b><i>magnus</i></b> <i>hito</i>\nТолько <b><i>magnus</i></b> маги могли использовать <b><i>magnus</i></b> руны"),
        new Rune("Kojo", Runes.kojo, cost:4, description:"...И вырастил он самое опасное и извивистое <b><i>kojo</i></b>\nХотя некоторые <b><i>kojo</i></b> съедобны, но те, что вызывают мастера рун скорее съедят вас"),
        new Rune("Lutum", Runes.lutum, cost:3, description:"Опасность призыва существ из <b><i>lutum</i></b> в том, что <b><i>lutum</i></b> можно найти почти везде под ногами.\nНикогда не знаешь это просто <b><i>lutum</i></b> или призванное существо"),
        new Rune("Celer", Runes.celer, cost:2, description:"Чтобы выжить, надо быть не просто <b><i>celer</i></b> и проворным, но и хитрым.\n...Призванное сущетсво великого мастера рун Альбедо, было одновремено <b><i>celer</i></b> и <i>magnus</i>"),
        new Rune("Yami", Runes.yami, cost:6, description:"Бойтесь <b><i>yami</i></b> что внутри вас...\nСущества <b><i>yami</i></b> имеют особый потенциал, но понять его дано не всем.\nСвет не поможет если вы встретите настоящий <b><i>yami</i></b>"),
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
