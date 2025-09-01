using System;

[Serializable]
public abstract class TimerBase
{
    public float Duration { get; protected set; }
    public float CurrentTime { get; protected set; }
    public float Progress { get; protected set; }

    protected float tickRate;

    public event Action OnTick;
    public event Action OnComplete;

    protected virtual void NotifyTick() => OnTick?.Invoke();
    protected virtual void FinishTimer() => OnComplete?.Invoke();
}
