using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IDataUI
{
    [SerializeField] private Text nameText;
    [SerializeField] private Button button;
    [SerializeField] private CameraData cameraData;

    private new Camera camera;
    private BuildingData buildingData;

    private void Start()
    {
        camera = cameraData.Camera;
        button.onClick.AddListener(OnClick);
    }

    public void SetData(ScriptableObject data)
    {
        buildingData = (BuildingData)data;
        nameText.text = buildingData.name;
    }

    private void OnClick()
    {
        var building = Instantiate(buildingData.Prefab);
        building.transform.position = new Vector3(4, 0, 0);

        if (buildingData.TowerData)
        {
            var tower = building.GetComponent<Tower>();
            if (tower)
                tower.SetData(buildingData.TowerData);
            else
                Debug.LogError("No Tower script on prefab.");
        }

        var screenPoint = camera.WorldToScreenPoint(building.transform.position);
        var buildingScript = building.GetComponent<Building>();
        buildingScript.Select(screenPoint);
    }
}
