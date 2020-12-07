using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
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

        Building.SelectedEvent += OnBuildingSelected;
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

    private void OnBuildingSelected(PointerEventData pointerEventData, Building building)
    {
        canvas.enabled = true;

        this.building = building;
        buildingTransform = building.transform;
        buildingOffset = buildingTransform.position - pointerEventData.pointerCurrentRaycast.worldPosition;

        rectTransform.position = pointerEventData.position;

        Debug.LogWarning($"PV-BUILDING SELECTED worldpos:{pointerEventData.pointerCurrentRaycast.worldPosition}, offset: {buildingOffset}");
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        building.OnMove();
        uiOffset = (Vector2)rectTransform.position - pointerEventData.position;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        // var newPosition = pointerEventData.pointerCurrentRaycast.worldPosition + buildingOffset;
        // buildingTransform.position = new Vector3(newPosition.x, building.YPosition, newPosition.z);

        rectTransform.position = pointerEventData.position + uiOffset;

        Debug.LogWarning($"PV-DRAG worldpos:{pointerEventData.pointerCurrentRaycast.worldPosition}, go:{pointerEventData.pointerCurrentRaycast.gameObject}, newPos: {buildingTransform.position}");

        // var camera = canvas.worldCamera;
        Debug.LogWarning($"PV-MA RAY camera {camera}");
        Ray ray = camera.ScreenPointToRay(pointerEventData.position);
        // Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        // if (Input.GetMouseButtonDown(0) && GUIUtility.hotControl == 0)
        // {
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            // hit.collider.gameObject;
            Debug.LogWarning($"PV-MA RAY hit {hit}, trans:{hit.transform}, :{hit.transform.gameObject}");

            var newPosition = hit.point + buildingOffset;
            buildingTransform.position = new Vector3(newPosition.x, building.YPosition, newPosition.z);
        }
        // }

        Debug.LogWarning($"PV-MA RAY mouse:{Input.GetMouseButtonDown(0)}, hot:{GUIUtility.hotControl}");
    }
}
