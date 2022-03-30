using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    public List<Runes> currentSpell = new List<Runes>();

    public PlayerData data;

    [SerializeField] GameObject prefab;
    
    int mana;
    int hp;

    private void Start()
    {
        Singleton = this;
        data = SaveSystem.LoadData();
        if (data == null)
        {
            data = new PlayerData();
        }

        hp = data.maxHP;
        mana = data.maxMana;
    }

    public void Cast()
    {
        foreach (Spell spell in Spell.Spells)
        {
            if (currentSpell.SequenceEqual(spell.spellRunes))
            {
                spell.Cast.Invoke();
                return;
            }
        }
        Debug.Log("No such spell");
    }

    public void Summon()
    {
        Unit unit = new Unit(speed: 2);
        var temp = Instantiate(prefab, Gameplay.Singleton.field);
        temp.GetComponent<RectTransform>().anchoredPosition = Gameplay.Singleton.squares[0].anchoredPosition;
        var unitControler = temp.GetComponent<UnitController>();

        unitControler.stats = unit;
        unitControler.currentSquare = 0;
        unitControler.playerControl = true;
        Gameplay.Singleton.playerUnits.Add(unitControler);
        Gameplay.Singleton.squares[unitControler.currentSquare].unitOn = unitControler;
    }

    public void Summon(Unit unit)
    {
        var temp = Instantiate(prefab, Gameplay.Singleton.field);
        temp.GetComponent<RectTransform>().anchoredPosition = Gameplay.Singleton.squares[0].anchoredPosition;
        var unitControler = temp.GetComponent<UnitController>();

        unitControler.stats = unit;
        unitControler.currentSquare = 0;
        unitControler.playerControl = true;
        Gameplay.Singleton.playerUnits.Add(unitControler);
        Gameplay.Singleton.squares[unitControler.currentSquare].unitOn = unitControler;
    }

    public void SummonEnemy(Unit unit)
    {
        var temp = Instantiate(prefab, Gameplay.Singleton.field);
        temp.GetComponent<RectTransform>().anchoredPosition = Gameplay.Singleton.squares[Gameplay.Singleton.squares.Count-1].anchoredPosition;
        var unitControler = temp.GetComponent<UnitController>();

        //only for debug
        temp.GetComponent<Image>().color = Color.red;
        //end
        
        unitControler.stats = unit;
        unitControler.currentSquare = Gameplay.Singleton.squares.Count-1;
        unitControler.playerControl = false;
        Gameplay.Singleton.enemyUnits.Add(unitControler);
        Gameplay.Singleton.squares[unitControler.currentSquare].unitOn = unitControler;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            //GameOver
        }
        Debug.Log($"Now hp is {hp}");
    }
}
