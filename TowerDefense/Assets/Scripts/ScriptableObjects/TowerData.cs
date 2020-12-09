using System;
using UnityEngine;

public enum DamageType
{
    Health, Speed
}

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/TowerData")]
[Serializable]
public class TowerData : ScriptableObject
{
    public float Interval = 2;
    public float Damage;
    public int SimultaneousTargets = 1;
    public DamageType DamageType = DamageType.Health;
    public Blast BlastPrefab;

    public override string ToString()
    {
        return $"Interval:{Interval} Damage:{Damage} Targets:{SimultaneousTargets} Blast{BlastPrefab}";
    }
}
