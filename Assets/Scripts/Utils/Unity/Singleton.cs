using UnityEngine;
 
/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    static bool shuttingDown = false;
    static object _lock = new object(); // Amschel - this lock is overkill for us but makes it thread safe
    private static T instance;
 
    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }
 
            lock (_lock)
            {
                if (instance == null)
                {
                    // Search for existing instance.
                    instance = (T)FindObjectOfType(typeof(T));
 
                    // Create new instance if one doesn't already exist.
                    if (instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
 
                        // Amschel - Persistence probably not required for CogBatTab
                        // unless we want it to upload data while the user switches scenes?
                        // // Make instance persistent.
                        // DontDestroyOnLoad(singletonObject);
                    }
                }
 
                return instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = gameObject.GetComponent<T>();
        }
    }
 
 
    void OnApplicationQuit()
    {
        shuttingDown = true;
    }
 
 
    void OnDestroy()
    {
        shuttingDown = true;
    }
}
