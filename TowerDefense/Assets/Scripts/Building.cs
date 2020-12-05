using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 offset;
    private float yPosition;
    private bool selected;

    private void Start()
    {
        yPosition = transform.position.y;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        selected = true;
        offset = transform.position - pointerEventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        selected = false;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        var newPosition = pointerEventData.pointerCurrentRaycast.worldPosition + offset;
        transform.position = new Vector3(newPosition.x, yPosition, newPosition.z);
    }
}
