using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent")]
[Serializable]
public class GameEvent : ScriptableObject
{
    public List<Action> actions = new List<Action>();

    public void Listen(Action action)
    {
        actions.Add(action);
    }

    public void Unlisten(Action action)
    {
        actions.Remove(action);
    }

    public void Trigger()
    {
        for (int i = actions.Count - 1; i >= 0; i--)
        {
            actions[i].Invoke();
        }
    }

    public override string ToString()
    {
        return $"GameEvent {name}";
    }
}
