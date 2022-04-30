using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public string unitName { get; }
    public int hp { get; }
    public int damage { get; }
    public int speed { get; }
    public int range { get; }
    public Sprite sprite { get; }
    public DamageType attackType { get; }
    public DamageType armorType { get; }

    public Unit(int hp = 10, int damage = 1, int speed = 1, int range = 1, Sprite sprite = null, DamageType attackType = DamageType.physical, DamageType armorType = DamageType.physical)
    {
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.sprite = sprite;
        this.range = range;
        this.attackType = attackType;
        this.armorType = armorType;
    }

    static float[,] modifiers = new float[8, 8]{
        { .5f, .3f, 1.2f, 1f, .8f, .8f, .8f, 1.5f },
        { 1.7f, .5f, .3f, .3f, .8f, .8f, .8f, 1.2f },
        { .8f, 1.2f, .5f, 1f, .8f, .8f, .8f, 1.2f },
        { 1f, 1.7f, 1f, .5f, .8f, .8f, .8f, 1.2f },
        { 1f, 1f, 1f, 1f, .3f, 2f, 1f, 1.3f },
        { 1f, 1f, 1f, 1f, 2f, .3f, 1f, 1.3f },
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },
        { 1f, 1.2f, 1.2f, 1.2f, .8f, .8f, 1f, 1f }};

    public static float GetDamageModifier(DamageType attack, DamageType defence)
    {
        return modifiers[(int)attack, (int)defence];
    }
}


public enum DamageType
{
    fire,
    water,
    air,
    electric,
    dark,
    light,
    energy,
    physical,
    NumberOf
}