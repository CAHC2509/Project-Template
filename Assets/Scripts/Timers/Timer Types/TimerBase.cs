using System;
using System.Collections.Generic;

[Serializable]
public abstract class TimerBase
{
    public float Duration { get; protected set; }
    public float CurrentTime { get; protected set; }
    public float Progress { get; protected set; }

    protected float tickRate;
    protected float lastTickTime;

    public event Action OnTick;
    public event Action OnComplete;

    protected virtual void NotifyTick() => OnTick?.Invoke();
    protected virtual void FinishTimer() => OnComplete?.Invoke();

    public abstract IEnumerator<float> TimerCoroutine();
}
