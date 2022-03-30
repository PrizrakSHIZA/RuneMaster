using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneButton : MonoBehaviour
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
            if (PlayerController.Singleton.currentSpell.Count <= 3)
            { 
                PlayerController.Singleton.currentSpell.Add(rune);
                RuneBook.Singleton.PlaceRune(rune);
            }
        }
        else
        {
            PlayerController.Singleton.currentSpell.Remove(rune);
            Destroy(gameObject);
        }
    }
}
