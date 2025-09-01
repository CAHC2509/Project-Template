using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class CountUpTimer : TimerBase
{
    private float lastTickTime;

    public CountUpTimer(float duration, float tickRate)
    {
        Duration = duration;
        this.tickRate = tickRate;
        Progress = 0f;
    }

    public IEnumerator TimerCoroutine()
    {
        lastTickTime = 0f;

        while (CurrentTime < Duration)
        {
            CurrentTime += Time.deltaTime;
            Progress = Mathf.Clamp01(CurrentTime / Duration);

            if (CurrentTime - lastTickTime >= tickRate)
            {
                NotifyTick();
                lastTickTime = CurrentTime;
            }

            yield return null;
        }

        Progress = 1f;
        FinishTimer();
    }
}
