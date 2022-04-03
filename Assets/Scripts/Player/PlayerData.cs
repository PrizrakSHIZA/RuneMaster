using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        };
        unlockedRunes = Enum.GetValues(typeof(Runes)).Cast<Runes>().ToList();
        unlockedRunes.Remove(Runes.empty);
        unlockedRunes.Remove(Runes.NumberOf);
        maxHP = 10;
        maxMana = 10;
    }
}
