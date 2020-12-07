using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public static event Action<PointerEventData, Building> SelectedEvent;

    [NonSerialized] public float YPosition;

    private void Start()
    {
        YPosition = transform.position.y;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        var offset = transform.position - pointerEventData.pointerCurrentRaycast.worldPosition;

        SelectedEvent?.Invoke(pointerEventData, this);
    }

    public void OnConfirm()
    {
        Debug.LogWarning($"PV-ONCOFIRM");
    }

    public void OnMove()
    {
        Debug.LogWarning($"PV-MOVE");
    }

    public void OnCancel()
    {
        Debug.LogWarning($"PV-DESTROY");
        Destroy(gameObject);
    }
}
