using System.Collections.Generic;
using UnityEngine;
using MEC;

[RequireComponent(typeof(PooledObject))]
public class AutomaticPoolReturning : MonoBehaviour
{
    private CoroutineHandle deactivationCoroutine;
    private PooledObject pooledObject;

    private void Awake()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    public void StartDeactivationCoroutine(float deactivationDelay)
    {
        StopDeactivationCoroutine();
        deactivationCoroutine = Timing.RunCoroutine(DeactivationCoroutine(deactivationDelay).CancelWith(gameObject));
    }

    public void StopDeactivationCoroutine()
    {
        if (deactivationCoroutine.IsValid)
        {
            Timing.KillCoroutines(deactivationCoroutine);
            deactivationCoroutine = default;
        }
    }

    private IEnumerator<float> DeactivationCoroutine(float deactivationDelay)
    {
        yield return Timing.WaitForSeconds(deactivationDelay);
        pooledObject.Release();
        deactivationCoroutine = default;
    }
}
