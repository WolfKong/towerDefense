using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/TowerData")]
[Serializable]
public class TowerData : ScriptableObject
{
    public float Interval = 2;
    public float Damage;
    public int SimultaneousTargets = 1;
    public GameObject Prefab;
    public Blast BlastPrefab;

    public override string ToString()
    {
        return $"Interval:{Interval} Damage:{Damage} Targets:{SimultaneousTargets} Tower:{Prefab} Blast{BlastPrefab}";
    }
}
