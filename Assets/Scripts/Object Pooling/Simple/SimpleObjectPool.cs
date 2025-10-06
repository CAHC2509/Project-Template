using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    [SerializeField] protected SimplePooledObject objectToPool;
    [SerializeField] protected int initialPoolSize = 10;

    private List<SimplePooledObject> pool;

    public void Initialize()
    {
        SetupPool();
    }

    public void Conclude()
    {
        CleanPool();
    }

    private void SetupPool()
    {
        pool = new List<SimplePooledObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            SimplePooledObject instance = Instantiate(objectToPool, transform);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            pool.Add(instance);
        }
    }

    private void CleanPool()
    {
        foreach (SimplePooledObject pooledObject in pool)
            Destroy(pooledObject.gameObject);

        pool.Clear();
    }

    public SimplePooledObject GetPooledObject()
    {
        foreach (SimplePooledObject pooledObject in pool)
        {
            if (!pooledObject.gameObject.activeInHierarchy)
                return pooledObject;
        }

        SimplePooledObject newInstance = Instantiate(objectToPool, transform);
        pool.Add(newInstance);
        return newInstance;
    }

    public void ReturnToPool(SimplePooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }
}
