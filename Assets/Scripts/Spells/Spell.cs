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
        #region summon spells
        //Demons
        { global::Spells.S_DemonGreat, new SpellSummon(Runes.magnus, Runes.igni, Cast: (bool player) =>
        {
            Unit unit = new Unit(9, 6, 1, sprite: Resources.Load<Sprite>("Units/Demon_02"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_DemonFast, new SpellSummon(Runes.celer, Runes.igni, Cast: (bool player) =>
        {
            Unit unit = new Unit(8, 3, 2, 3, sprite: Resources.Load<Sprite>("Units/Demon_01"));

            Gameplay.Singleton.Summon(unit, player);
        })},
        //Darkness
        { global::Spells.S_DarkGreat, new SpellSummon(Runes.magnus, Runes.yami, Cast: (bool player) =>
        {
            Unit unit = new Unit(12, 7, 1, sprite: Resources.Load<Sprite>("Units/Dark_01"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_DarkFast, new SpellSummon(Runes.celer, Runes.yami, Cast: (bool player) =>
        {
            Unit unit = new Unit(10, 5, 3, sprite: Resources.Load<Sprite>("Units/Dark_02"));

            Gameplay.Singleton.Summon(unit, player);
        })},
        //Dirt
        { global::Spells.S_Dirt, new SpellSummon(Runes.lutum, Cast: (bool player) =>
        {
            Unit unit = new Unit(3, 2, 1, sprite: Resources.Load<Sprite>("Units/Dirt_01"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_DirtFast, new SpellSummon(Runes.celer, Runes.lutum, Cast: (bool player) =>
        {
            Unit unit = new Unit(5, 3, 2, sprite: Resources.Load<Sprite>("Units/Dirt_02"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_DirtGreat, new SpellSummon(Runes.magnus, Runes.lutum, Cast: (bool player) =>
        {
            Unit unit = new Unit(6, 6, 1, sprite: Resources.Load<Sprite>("Units/Dirt_03"));

            Gameplay.Singleton.Summon(unit, player);
        })},
        //Plants
        { global::Spells.S_Plant, new SpellSummon(Runes.kojo, Cast: (bool player) =>
        {
            Unit unit = new Unit(2, 5, 1, sprite: Resources.Load<Sprite>("Units/Plants_01"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_PlantFast, new SpellSummon(Runes.celer, Runes.kojo, Cast: (bool player) =>
        {
            Unit unit = new Unit(3, 7, 2, sprite: Resources.Load<Sprite>("Units/Plants_02"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_PlantGreat, new SpellSummon(Runes.magnus, Runes.kojo, Cast: (bool player) =>
        {
            Unit unit = new Unit(4, 8, 2, sprite: Resources.Load<Sprite>("Units/Plants_03"));

            Gameplay.Singleton.Summon(unit, player);
        })},
        //Humans
        { global::Spells.S_Human, new SpellSummon(Runes.hito, Cast: (bool player) =>
        {
            Unit unit = new Unit(4, 2, 2, sprite: Resources.Load<Sprite>("Units/Human_01"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_HumanFast, new SpellSummon(Runes.celer, Runes.hito, Cast: (bool player) =>
        {
            Unit unit = new Unit(6, 3, 3, sprite: Resources.Load<Sprite>("Units/Human_02"));

            Gameplay.Singleton.Summon(unit, player);
        })},

        { global::Spells.S_HumanGreat, new SpellSummon(Runes.magnus, Runes.hito, Cast: (bool player) =>
        {
            Unit unit = new Unit(8, 4, 2, sprite: Resources.Load<Sprite>("Units/Human_03"));

            Gameplay.Singleton.Summon(unit, player);
        })},
        #endregion
        #region target spells
        { global::Spells.T_CastFire, new SpellTarget(Runes.igni, Cast:(bool player, int target) =>
        {
            Gameplay.Singleton.SpawnParticle("FireBlow", target);
            if(Gameplay.Singleton.squares[target].unitOn != null)
                Gameplay.Singleton.squares[target].unitOn.TakeDamage(5, DamageType.fire);
        })},
        { global::Spells.T_Fireball, new SpellTarget(Runes.igni, Runes.igni, Cast:(bool player, int target) =>
        {
            Gameplay.Singleton.SpawnParticle("FireBlow", target);
            if(Gameplay.Singleton.squares[target].unitOn != null)
                Gameplay.Singleton.squares[target].unitOn.TakeDamage(15, DamageType.fire);
        })},
        { global::Spells.T_FireSplash, new SpellTarget(Runes.igni, Runes.igni, Runes.igni, Cast:(bool player, int target) =>
        {
            for (int i = -1; i < 2; i++)
            {
                Gameplay.Singleton.SpawnParticle("FireBlow", target + i);
                if(Gameplay.Singleton.squares[target + i].unitOn != null)
                    Gameplay.Singleton.squares[target + i].unitOn.TakeDamage(15, DamageType.fire);
            }
        })},
        #endregion
    };
}


public enum Spells
{ 
    //Demons
    S_DemonGreat,
    S_DemonFast,
    //Darkness
    S_DarkGreat,
    S_DarkFast,
    //Dirt
    S_Dirt,
    S_DirtFast,
    S_DirtGreat,
    //Plants
    S_Plant,
    S_PlantFast,
    S_PlantGreat,
    //Humans
    S_Human,
    S_HumanFast,
    S_HumanGreat,
    // ---- target spells
    //Fire
    T_CastFire,
    T_Fireball,
    T_FireSplash,
    NumberOf,
}

public class SpellSummon : Spell
{
    public Action<bool> Cast;

    public SpellSummon(Runes rune1 = Runes.empty, Runes rune2 = Runes.empty, Runes rune3 = Runes.empty, Action<bool> Cast = null) : base(rune1, rune2 , rune3)
    { 
        this.Cast = Cast;
    }
}

public class SpellTarget : Spell
{
    public Action<bool, int> Cast;

    public SpellTarget(Runes rune1 = Runes.empty, Runes rune2 = Runes.empty, Runes rune3 = Runes.empty, Action<bool, int> Cast = null) : base(rune1, rune2, rune3)
    {
        this.Cast = Cast;
    }
}