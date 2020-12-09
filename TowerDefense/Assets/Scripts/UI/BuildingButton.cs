using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IDataUI
{
    [SerializeField] private Text nameText;
    [SerializeField] private Button button;

    private BuildingData buildingData;

    private void Start()
    {
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

        if (buildingData.TowerData)
        {
            var tower = building.GetComponent<Tower>();
            if (tower)
                tower.SetData(buildingData.TowerData);
            else
                Debug.LogError("No Tower script on prefab.");
        }

        var buildingScript = building.GetComponent<Building>();
        buildingScript.Select(new Vector2(Screen.width, Screen.height) * 0.5f);
    }
}
