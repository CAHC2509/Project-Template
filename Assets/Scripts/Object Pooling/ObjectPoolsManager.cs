using UnityEngine;

public class ObjectPoolsManager : MonoBehaviour
{
    [SerializeField] private Transform objectPoolsParent;

    private ObjectPool[] objectPools;

    private void Awake()
    {
        objectPools = objectPoolsParent.GetComponentsInChildren<ObjectPool>();
    }

    public void Initialize()
    {
        foreach (ObjectPool pool in objectPools)
            pool.Initialize();
    }

    public void Conclude()
    {
        foreach (ObjectPool pool in objectPools)
            pool.Conclude();
    }
}
