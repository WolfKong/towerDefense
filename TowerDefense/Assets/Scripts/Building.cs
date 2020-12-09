using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Vector2, Building> SelectedEvent;

    [NonSerialized] public float YPosition;

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        YPosition = transform.localScale.y;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Select(pointerEventData.position);
    }

    public void Select(Vector2 pointerPosition)
    {
        SelectedEvent?.Invoke(pointerPosition, this);
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
