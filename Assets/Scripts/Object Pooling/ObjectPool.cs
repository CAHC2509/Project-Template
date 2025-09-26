using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected PooledObject objectToPool;
    [SerializeField] protected int initialPoolSize = 10;
    
    protected Stack<PooledObject> stack;

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
        stack = new Stack<PooledObject>();
        PooledObject instance = null;

        for (int i = 0; i < initialPoolSize; i++)
        {
            instance = Instantiate(objectToPool, transform);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    private void CleanPool()
    {
        foreach (PooledObject pooledObject in stack)
            Destroy(pooledObject.gameObject);

        stack.Clear();
    }

    public PooledObject GetPooledObject()
    {
        if (stack.Count <= 0)
        {
            PooledObject newInstance = Instantiate(objectToPool, transform);
            newInstance.Pool = this;
            return newInstance;
        }

        PooledObject nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        stack.Push(pooledObject);
    }
}
