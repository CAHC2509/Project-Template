using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SelectableStateBase
{
    protected SelectableData selectableData;
    protected Image[] mainImages;
    protected Image[] secondaryImages;
    protected TextMeshProUGUI[] texts;
    protected Transform container;

    private Coroutine scalingCoroutine;
    private MonoBehaviour runner;

    protected SelectableStateBase(MonoBehaviour runner)
    {
        this.runner = runner;
    }

    protected void StartScaleAnimation(Transform target, Vector3 initialScale, Vector3 finalScale, float scaleDuration)
    {
        if (scalingCoroutine != null)
            runner.StopCoroutine(scalingCoroutine);

        scalingCoroutine = runner.StartCoroutine(ScalingCoroutine(target, initialScale, finalScale, scaleDuration));
    }

    protected void StopScalingAnimation()
    {
        if (scalingCoroutine != null)
            runner.StopCoroutine(scalingCoroutine);

        scalingCoroutine = null;
    }

    private IEnumerator ScalingCoroutine(Transform target, Vector3 initialScale, Vector3 finalScale, float scaleDuration)
    {
        float currentDuration = 0f;

        while (currentDuration < scaleDuration)
        {
            target.localScale = Vector3.Lerp(initialScale, finalScale, currentDuration / scaleDuration);
            currentDuration += Time.unscaledDeltaTime;
            yield return null;
        }

        target.localScale = finalScale;
        scalingCoroutine = null;
    }
}
