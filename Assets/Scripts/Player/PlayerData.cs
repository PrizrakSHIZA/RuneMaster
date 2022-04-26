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
    public int manaRegen;

    public PlayerData(List<Runes> unlockedRunes)
    {
        this.unlockedRunes = unlockedRunes;
    }

    public PlayerData()
    {
        unlockedRunes = new List<Runes>() 
        {  
        };
        #region temporary
        unlockedRunes = Enum.GetValues(typeof(Runes)).Cast<Runes>().ToList();
        unlockedRunes.Remove(Runes.empty);
        unlockedRunes.Remove(Runes.NumberOf);
        #endregion
        maxHP = 20;
        maxMana = 15;
        manaRegen = 2;
    }
}
