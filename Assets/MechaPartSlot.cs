using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MechaPartSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public MechaSlotType type;
    public MechaTorso torso = MechaTorso.None;
    public MechaArms arm = MechaArms.None;
    public MechaLegs leg = MechaLegs.None;
    public Image Icon;
    public bool isPlayer = false;

    public Action<MechaPartSlot> OnPointerDownAction;
    public Action OnBeginDragAction;
    public Action OnDragAction;
    public Action OnDragEndAction;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragAction?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragAction?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEndAction?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke(this);
    }
}

public enum MechaSlotType
{
    Torso = 0,
    LeftArm = 1,
    RightArm = 2,
    MiddleArm = 3,
    Leg = 4,
}
