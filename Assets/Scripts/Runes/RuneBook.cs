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

    #region Description
    [Space()]
    [Header("Description")]
    [SerializeField] Image runeimage;
    [SerializeField] Text name;
    [SerializeField] Text desc;
    #endregion

    List<Runes> runeList = new List<Runes>();

    public List<GameObject> addedRunes = new List<GameObject>();

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
        addedRunes.Add(temp);
    }

    public void ClearRunes()
    {
        foreach (GameObject go in addedRunes)
        {
            Destroy(go);
        }
        addedRunes.Clear();
    }


    #region Descriptiobn
    public void ShowDescription(Rune rune)
    {
        runeimage.sprite = rune.sprite;
        runeimage.color = new Color(255, 255, 255, 1f);
        name.text = rune.name;
        desc.text = rune.description;
    }

    public void ClearDescription()
    {
        runeimage.sprite = null;
        runeimage.color = new Color(255, 255, 255, 0.3f);
        name.text = "";
        desc.text = "";
    }
    #endregion
}
