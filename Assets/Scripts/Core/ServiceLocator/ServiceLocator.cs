using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Service Locator for dependency injection
/// Register managers here for global access throughout the project
/// Usage: ServiceLocator.Register<IService>(serviceInstance);
///        var service = ServiceLocator.Get<IService>();
/// </summary>
public class ServiceLocator : Singleton<ServiceLocator>
{
    private Dictionary<System.Type, IService> _services = new Dictionary<System.Type, IService>();

    public void Register<T>(T service) where T : class, IService
    {
        System.Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type.Name} is already registered!");
            return;
        }
        _services[type] = service;
        service.Initialize();
    }

    public T Get<T>() where T : class, IService
    {
        System.Type type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            Debug.LogError($"Service of type {type.Name} is not registered in ServiceLocator!");
            return null;
        }
        return _services[type] as T;
    }

    public void Unregister<T>() where T : class, IService
    {
        System.Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services[type].Shutdown();
            _services.Remove(type);
        }
    }

    public void UnregisterAll()
    {
        foreach (var service in _services.Values)
        {
            service.Shutdown();
        }
        _services.Clear();
    }
}
