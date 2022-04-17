using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AI", menuName = "AI")]
public class AI_SO : ScriptableObject
{
    public List<Spells> spellList;

    [Range(0, 10)]
    public int smartChance;

    public int HP;
    public int mana;
}
