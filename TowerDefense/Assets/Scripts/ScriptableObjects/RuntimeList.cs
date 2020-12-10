using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "ScriptableObjects/RuntimeList")]
[Serializable]
public class RuntimeList : ScriptableObject
{
    public List<GameObject> Items;

    public override string ToString()
    {
        return $"RuntimeList - Items\n{string.Join(",\n", Items)}";
    }
}
