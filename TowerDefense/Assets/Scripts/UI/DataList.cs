using System.Collections.Generic;
using UnityEngine;

public class DataList<T, SO> : MonoBehaviour where T : IDataUI where SO : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private string folderName;

    private List<SO> dataList = new List<SO>();

    private void Awake()
    {
        var objects = Resources.LoadAll($"Data/{folderName}", typeof(SO));

        foreach (var obj in objects)
            dataList.Add((SO)obj);

        dataList.Sort(CompareData);
    }

    private void Start()
    {
        foreach (var data in dataList)
        {
            var dataUI = Instantiate(prefab, transform).GetComponent<T>();

            if (dataUI == null)
                Debug.LogWarning($"Component {typeof(T)} Not Found on {prefab.name}!");
            else
                dataUI.SetData(data);
        }
    }

    protected virtual int CompareData(SO a, SO b)
    {
        return a.name.CompareTo(b.name);
    }
}
