using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPosition
{
    Bottom, Middle, Top
}

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveData")]
[Serializable]
public class WaveData : ScriptableObject
{
    public SpawnPosition SpawnPosition;
    public List<MiniWaveData> MiniWaves;
    public float IntervalBetweenMiniWaves;

    public override string ToString()
    {
        return $"SpawnPosition:{SpawnPosition}, Interval:{IntervalBetweenMiniWaves},\nMiniWaves:\n{string.Join(",\n", MiniWaves)}";
    }
}
