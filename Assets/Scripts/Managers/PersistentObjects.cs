using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    private void Awake()
    {
        PersistentObjects[] persistenObjects = FindObjectsByType<PersistentObjects>(FindObjectsSortMode.None);

        if (persistenObjects.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}