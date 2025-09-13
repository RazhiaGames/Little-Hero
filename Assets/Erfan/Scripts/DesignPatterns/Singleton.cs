using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool isDontDestroyOnLoad = false;
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    Debug.LogError($"No instance of {typeof(T)} found in the scene.");
                }
            }
    
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (isDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        
    }
}