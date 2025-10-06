using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected PooledObject objectToPool;
    [SerializeField] protected int initialPoolSize = 10;
    [SerializeField] protected int maxPoolSize = 20;

    protected Stack<PooledObject> freeStack;
    protected Queue<PooledObject> allQueue;

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
        freeStack = new Stack<PooledObject>();
        allQueue = new Queue<PooledObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            PooledObject instance = Instantiate(objectToPool, transform);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            freeStack.Push(instance);
            allQueue.Enqueue(instance);
        }
    }

    private void CleanPool()
    {
        foreach (PooledObject pooledObject in allQueue)
            Destroy(pooledObject.gameObject);

        freeStack.Clear();
        allQueue.Clear();
    }

    public PooledObject GetPooledObject()
    {
        if (freeStack.Count > 0)
        {
            PooledObject next = freeStack.Pop();
            next.gameObject.SetActive(true);
            return next;
        }

        if (allQueue.Count < maxPoolSize)
        {
            PooledObject newInstance = Instantiate(objectToPool, transform);
            newInstance.Pool = this;
            allQueue.Enqueue(newInstance);
            return newInstance;
        }

        PooledObject reused = allQueue.Dequeue();
        allQueue.Enqueue(reused);
        reused.gameObject.SetActive(true);
        return reused;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        freeStack.Push(pooledObject);
    }
}
