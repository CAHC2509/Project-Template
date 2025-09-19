using System.Collections;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private float deactivationDelay = 3f;

    private Coroutine deactivationCoroutine;
    private ObjectPool pool;

    public ObjectPool Pool { get => pool; set => pool = value; }

    private IEnumerator DeactivationCoroutine()
    {
        yield return new WaitForSeconds(deactivationDelay);
        Release();
    }

    public void StartDeactivationCoroutine()
    {
        if (deactivationCoroutine != null)
            StopCoroutine(deactivationCoroutine);

        deactivationCoroutine = StartCoroutine(DeactivationCoroutine());
    }

    public void Release()
    {
        pool.ReturnToPool(this);
    }
}
