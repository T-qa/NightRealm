using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base manager class for all systems
/// Provides common manager functionality
/// </summary>
public abstract class BaseManager : MonoBehaviourSingleton<BaseManager>
{
    protected bool isInitialized = false;

    protected virtual void Start()
    {
        if (!isInitialized)
        {
            Initialize();
        }
    }

    public virtual void Initialize()
    {
        isInitialized = true;
    }

    public virtual void Shutdown()
    {
        isInitialized = false;
    }
}
