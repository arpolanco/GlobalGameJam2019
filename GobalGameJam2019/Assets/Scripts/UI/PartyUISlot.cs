using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartyUISlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform panel = transform as RectTransform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(panel, Input.mousePosition,null))
            Debug.Log("In slot 0");
    }


}
