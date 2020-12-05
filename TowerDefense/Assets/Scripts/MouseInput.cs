using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Vector3> Clicked;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Clicked?.Invoke(pointerEventData.pointerCurrentRaycast.worldPosition);
    }
}
