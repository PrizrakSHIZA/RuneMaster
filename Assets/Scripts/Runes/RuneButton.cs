using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Runes rune;
    public bool inBook = true;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickHandle);
    }

    void OnClickHandle()
    {
        if (inBook)
        {
            if (PlayerController.Singleton.currentSpell.Count < 3)
            { 
                PlayerController.Singleton.currentSpell.Add(rune);
                RuneBook.Singleton.PlaceRune(rune);
            }
        }
        else
        {
            PlayerController.Singleton.currentSpell.Remove(rune);
            RuneBook.Singleton.addedRunes.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RuneBook.Singleton.ShowDescription(Rune.GetRune(rune));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RuneBook.Singleton.ClearDescription();
    }
}
