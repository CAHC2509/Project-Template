using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;

[Serializable]
public class CountUpTimer : TimerBase
{
    public CountUpTimer(float duration, float tickRate)
    {
        Duration = duration;
        this.tickRate = tickRate;
        Progress = 0f;
    }

    public override IEnumerator<float> TimerCoroutine()
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

            yield return Timing.WaitForOneFrame;
        }

        Progress = 1f;
        FinishTimer();
    }
}
