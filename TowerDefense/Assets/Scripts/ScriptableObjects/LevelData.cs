using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
[Serializable]
public class LevelData : ScriptableObject
{
    public int GoalHealth;

    public override string ToString()
    {
        return $"GoalHealth:{GoalHealth}";
    }
}
