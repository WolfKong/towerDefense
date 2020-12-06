using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPoint
{
    Bottom, Middle, Top
}

[CreateAssetMenu(fileName = "MiniWave", menuName = "ScriptableObjects/MiniWaveData")]
[Serializable]
public class MiniWaveData : ScriptableObject
{
    public SpawnPoint SpawnPoint;
    public List<EnemyData> Enemies;
    public float IntervalBetweenSpawns;

    public override string ToString()
    {
        return $"SpawnPoint:{SpawnPoint}, Interval:{IntervalBetweenSpawns},\nEnemies:{string.Join(",\n", Enemies)}";
    }
}
