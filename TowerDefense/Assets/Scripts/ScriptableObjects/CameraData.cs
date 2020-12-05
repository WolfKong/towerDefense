using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/CameraData")]
[Serializable]
public class CameraData : ScriptableObject
{
    public Transform Transform;

    public override string ToString()
    {
        return $"CameraTransform:{Transform}";
    }
}
