using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public string unitName { get; }
    public int hp { get; }
    public int damage { get; }
    public int speed { get; }
    public Sprite sprite { get; }
    public Base body { get; }
    public DamageType attackType { get; }
    public DamageType armorType { get; }

    public Unit(int hp = 10, int damage = 1, int speed = 1, Sprite sprite = null, Base body = Base.test, DamageType attackType = DamageType.earth, DamageType armorType = DamageType.earth)
    {
        this.hp = hp;
        this.damage = damage;
        this.speed = speed;
        this.sprite = sprite;
        this.body = body;
        this.attackType = attackType;
        this.armorType = armorType;
    }
}

public enum DamageType
{
    physical,
    fire,
    ice,
    wind,
    earth,
    NumberOf
}

public enum Base
{
    test,
    skeleton,
}