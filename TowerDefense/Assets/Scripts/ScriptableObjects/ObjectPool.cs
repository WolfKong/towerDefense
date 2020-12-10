using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool", menuName = "ScriptableObjects/ObjectPool")]
[Serializable]
public class ObjectPool : ScriptableObject
{
    public int Size;
    public GameObject Prefab;
    public List<GameObject> Items;

    private Transform Parent;

    private static Vector3 vector3Zero = Vector3.zero;

    public void CreatePool(Transform parent)
    {
        Parent = parent;

        Items = new List<GameObject>();

        for (var i = 0; i < Size; i++)
        {
            var item = Instantiate(Prefab, parent);
            item.SetActive(false);
            Items.Add(item);
        }
    }

    public GameObject GetObject()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            var item = Items[i];
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }

    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(Parent);
        gameObject.transform.localPosition = vector3Zero;
    }

    public override string ToString()
    {
        return $"ObjectPool - Size: {Size} Prefab: {Prefab}";
    }
}
