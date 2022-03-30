using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneBook : MonoBehaviour
{
    public static RuneBook Singleton;

    [SerializeField] Transform content;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform runePlace;

    List<Runes> runeList = new List<Runes>();

    private void Start()
    {
        Singleton = this;

        runeList = PlayerController.Singleton.data.unlockedRunes;
        foreach (Runes rune in runeList)
        {
            var temp = Instantiate(prefab, content);
            temp.GetComponent<Image>().sprite = Rune.GetRune(rune).sprite;
            temp.GetComponent<RuneButton>().rune = rune;
        }
    }

    public void PlaceRune(Runes rune)
    {
        var temp = Instantiate(prefab, runePlace);
        temp.GetComponent<Image>().sprite = Rune.GetRune(rune).sprite;
        temp.GetComponent<RuneButton>().rune = rune;
        temp.GetComponent<RuneButton>().inBook = false;
    }
}
