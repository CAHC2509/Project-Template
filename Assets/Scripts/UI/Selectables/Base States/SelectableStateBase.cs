using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;

public abstract class SelectableStateBase
{
    protected SelectableData selectableData;
    protected Image[] mainImages;
    protected Image[] secondaryImages;
    protected TextMeshProUGUI[] texts;
    protected Transform container;

    private CoroutineHandle scalingHandle;

    protected void StartScaleAnimation(Transform target, Vector3 initialScale, Vector3 finalScale, float scaleDuration)
    {
        if (scalingHandle.IsValid)
            Timing.KillCoroutines(scalingHandle);

        scalingHandle = Timing.RunCoroutine(ScalingCoroutine(target, initialScale, finalScale, scaleDuration));
    }

    protected void StopScalingAnimation()
    {
        if (scalingHandle.IsValid)
            Timing.KillCoroutines(scalingHandle);

        scalingHandle = default;
    }

    private IEnumerator<float> ScalingCoroutine(Transform target, Vector3 initialScale, Vector3 finalScale, float scaleDuration)
    {
        float currentDuration = 0f;

        while (currentDuration < scaleDuration)
        {
            target.localScale = Vector3.Lerp(initialScale, finalScale, currentDuration / scaleDuration);
            currentDuration += Time.unscaledDeltaTime;
            yield return Timing.WaitForOneFrame;
        }

        target.localScale = finalScale;
        scalingHandle = default;
    }
}
