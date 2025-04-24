using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public event Action OnClickEvent;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke();
    }
}
