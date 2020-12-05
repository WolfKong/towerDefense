using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
[Serializable]
public class EnemyData : ScriptableObject
{
    public float Speed;
    public int Health;
    public GameObject Prefab;

    public override string ToString()
    {
        return $"Speed:{Speed} HP:{Health} Prefab:{Prefab}";
    }
}
