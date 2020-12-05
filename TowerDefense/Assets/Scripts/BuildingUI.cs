using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button destroyButton;

    public static event Action ConfirmEvent;
    public static event Action MoveEvent;
    public static event Action DestroyEvent;

    void Start()
    {
        confirmButton.onClick.AddListener(() => ConfirmEvent?.Invoke());
        moveButton.onClick.AddListener(() => MoveEvent?.Invoke());
        destroyButton.onClick.AddListener(() => DestroyEvent?.Invoke());
    }
}
