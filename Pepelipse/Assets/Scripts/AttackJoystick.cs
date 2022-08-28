using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Action PointerDownEvent;

    public Action PointerUpEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDownEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUpEvent?.Invoke();
    }
}
