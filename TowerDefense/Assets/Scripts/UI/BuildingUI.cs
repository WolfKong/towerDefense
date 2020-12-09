using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button rotateButton;
    [SerializeField] private Canvas canvas;
    [SerializeField] private new Camera camera;

    private RectTransform rectTransform;
    private Vector2 uiOffset;
    private Building building;
    private Transform buildingTransform;
    private Vector3 buildingOffset;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas.enabled = false;

        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
        rotateButton.onClick.AddListener(Rotate);

        Building.SelectedEvent += OnBuildingSelected;
    }

    private void OnDestroy()
    {
        Building.SelectedEvent -= OnBuildingSelected;
    }

    private void Confirm()
    {
        building.OnConfirm();
        canvas.enabled = false;
    }

    private void Cancel()
    {
        building.OnCancel();
        canvas.enabled = false;
    }

    private void Rotate()
    {
        buildingTransform.eulerAngles += new Vector3(0, 90, 0);
    }

    private void OnBuildingSelected(Vector2 pointerPosition, Building building)
    {
        canvas.enabled = true;

        this.building = building;
        buildingTransform = building.transform;
        buildingOffset = Vector3.zero;

        rectTransform.position = pointerPosition;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        building.OnBeginDrag();
        uiOffset = (Vector2)rectTransform.position - pointerEventData.position;

        if (PointHitsFloor(pointerEventData, out var hit))
            buildingOffset = buildingTransform.position - hit.point;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        building.OnEndDrag();
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        rectTransform.position = pointerEventData.position + uiOffset;

        if (PointHitsFloor(pointerEventData, out var hit))
        {
            var newPosition = hit.point + buildingOffset;
            buildingTransform.position = new Vector3(newPosition.x, building.YPosition, newPosition.z);
        }
    }

    private bool PointHitsFloor(PointerEventData pointerEventData, out RaycastHit result)
    {
        result = new RaycastHit();
        Ray ray = camera.ScreenPointToRay(pointerEventData.position);

        var hits = Physics.RaycastAll(ray);
        if (hits.Length > 0)
        {
            var floorHit = hits.FirstOrDefault(h => h.transform.name == "Floor");

            if (floorHit.transform)
            {
                result = floorHit;
                return true;
            }
        }

        return false;
    }
}
