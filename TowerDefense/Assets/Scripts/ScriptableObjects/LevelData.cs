using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
[Serializable]
public class LevelData : ScriptableObject
{
    public List<MiniWaveData> Waves;
    public int GoalHealth;

    [NonSerialized] public int CurrentWave;

    public override string ToString()
    {
        return $"GoalHealth:{GoalHealth},\nWaves:\n{string.Join(",\n", Waves)}";
    }
}
