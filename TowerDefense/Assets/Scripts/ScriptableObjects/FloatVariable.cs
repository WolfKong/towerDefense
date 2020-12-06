using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "ScriptableObjects/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    public float Value;

    public override string ToString()
    {
        return $"Value:{Value}";
    }
}
