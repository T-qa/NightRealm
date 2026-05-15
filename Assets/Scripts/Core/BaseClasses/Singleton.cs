using UnityEngine;

/// <summary>
/// Generic singleton for non-MonoBehaviour classes
/// Usage: public class MyManager : Singleton<MyManager> { }
/// </summary>
public class Singleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    protected Singleton()
    {
    }
}
