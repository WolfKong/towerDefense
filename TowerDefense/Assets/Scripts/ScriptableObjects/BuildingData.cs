using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "ScriptableObjects/BuildingData")]
[Serializable]
public class BuildingData : ScriptableObject
{
    public int Cost;
    public GameObject Prefab;
    public TowerData TowerData;

    public override string ToString()
    {
        return $"Cost:{Cost} Building:{Prefab} TowerData:{TowerData}";
    }
}
