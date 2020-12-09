using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IDataUI
{
    [SerializeField] private Text nameText;
    [SerializeField] private Button button;

    private TowerData towerData;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetData(ScriptableObject data)
    {
        towerData = (TowerData)data;
        nameText.text = towerData.name;
    }

    private void OnClick()
    {
        var tower = Instantiate(towerData.Prefab).GetComponent<Tower>();
        tower.SetData(towerData);

        var building = tower.GetComponent<Building>();
        building.Select(new Vector2(Screen.width, Screen.height) * 0.5f);
    }
}
