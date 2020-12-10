using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
[Serializable]
public class LevelData : ScriptableObject
{
    public List<WaveData> Waves;
    public int GoalHealth;
    public int InitialInterval;
    public int IntervalBetweenWaves;

    [NonSerialized] public int CurrentWave;

    public int TotalWaves => Waves.Count;

    public override string ToString()
    {
        return $"GoalHealth:{GoalHealth}, Intervals:{InitialInterval}, {IntervalBetweenWaves}\nWaves:\n{string.Join(",\n", Waves)}";
    }
}
