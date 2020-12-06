using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData")]
[Serializable]
public class TowerData : ScriptableObject
{
    public float Interval;
    public float Damage;
    public GameObject Prefab;
    public Blast BlastPrefab;

    public override string ToString()
    {
        return $"Interval:{Interval} Damage:{Damage} Tower:{Prefab} Blast{BlastPrefab}";
    }
}
