using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base controller class for all game controllers
/// Provides common functionality like initialization and shutdown
/// </summary>
public abstract class BaseController : MonoBehaviour
{
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void LateUpdate() { }

    public virtual void Initialize() { }
    public virtual void Shutdown() { }
}
