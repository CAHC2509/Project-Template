using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class CountdownTimer : TimerBase
{
    public CountdownTimer(float duration, float tickRate)
    {
        Duration = duration;
        this.tickRate = tickRate;
        Progress = 1f;
    }

    public override IEnumerator TimerCoroutine()
    {
        CurrentTime = Duration;
        lastTickTime = Duration;

        while (CurrentTime > 0f)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime < 0f) CurrentTime = 0f;

            Progress = Mathf.Clamp01(CurrentTime / Duration);

            if (lastTickTime - CurrentTime >= tickRate)
            {
                NotifyTick();
                lastTickTime = CurrentTime;
            }

            yield return null;
        }

        Progress = 0f;
        FinishTimer();
    }
}
