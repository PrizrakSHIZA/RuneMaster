using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetButton : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        ExecuteEvents.Execute(PlayerController.Singleton.targets[0], new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }
}
