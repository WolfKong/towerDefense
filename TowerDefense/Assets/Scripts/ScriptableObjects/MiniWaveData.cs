using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPosition
{
    Bottom, Middle, Top
}

[CreateAssetMenu(fileName = "MiniWave", menuName = "ScriptableObjects/MiniWaveData")]
[Serializable]
public class MiniWaveData : ScriptableObject
{
    public SpawnPosition SpawnPosition;
    public List<EnemyData> Enemies;
    public float IntervalBetweenSpawns;

    public override string ToString()
    {
        return $"SpawnPosition:{SpawnPosition}, Interval:{IntervalBetweenSpawns},\nEnemies:\n{string.Join(",\n", Enemies)}";
    }
}
