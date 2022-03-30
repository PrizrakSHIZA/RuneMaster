using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<Runes> unlockedRunes;
    public int maxMana;
    public int maxHP;

    public PlayerData(List<Runes> unlockedRunes)
    {
        this.unlockedRunes = unlockedRunes;
    }

    public PlayerData()
    {
        unlockedRunes = new List<Runes>() 
        {
            Runes.test1,
            Runes.test2,
            Runes.test3,
        };
        maxHP = 10;
        maxMana = 10;
    }
}
