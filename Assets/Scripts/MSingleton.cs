using UnityEngine;

public class MSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public T Instance
    {
        get 
        {
            if (!instance)
            {
                // Search for existing before creating a new one.
                instance = GameObject.FindObjectOfType<T>();
                if (!instance)
                {
                    GameObject spawn = new GameObject(typeof(T).Name);
                    instance = spawn.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    private T instance;
}