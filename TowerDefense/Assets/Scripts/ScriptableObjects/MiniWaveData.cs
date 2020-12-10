using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiniWave", menuName = "ScriptableObjects/MiniWaveData")]
[Serializable]
public class MiniWaveData : ScriptableObject
{
    public List<EnemyData> Enemies;
    public float IntervalBetweenSpawns;

    public override string ToString()
    {
        return $"Interval:{IntervalBetweenSpawns},\nEnemies:\n{string.Join(",\n", Enemies)}";
    }
}
