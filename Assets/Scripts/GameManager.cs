using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnInitialization;
    public static event Action OnFinalization;

    private void Start()
    {
        OnInitialization?.Invoke();
    }

    private void OnDestroy()
    {
        OnFinalization?.Invoke();
    }
}