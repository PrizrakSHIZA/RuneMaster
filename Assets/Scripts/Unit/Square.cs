using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2 anchoredPosition { get { return GetComponent<RectTransform>().anchoredPosition; } }
    public UnitController unitOn;
}
