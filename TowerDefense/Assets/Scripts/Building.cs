using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public static event Action<PointerEventData, Building> SelectedEvent;

    [NonSerialized] public float YPosition;

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        YPosition = transform.position.y;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        var offset = transform.position - pointerEventData.pointerCurrentRaycast.worldPosition;

        SelectedEvent?.Invoke(pointerEventData, this);
    }

    public void OnConfirm()
    {
    }

    public void OnCancel()
    {
        Destroy(gameObject);
    }

    public void OnBeginDrag()
    {
        material.color = Color.cyan;
    }

    public void OnEndDrag()
    {
        material.color = Color.white;
    }
}
