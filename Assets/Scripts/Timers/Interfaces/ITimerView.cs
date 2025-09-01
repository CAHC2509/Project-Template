using UnityEngine;

public interface ITimerView : IViewBase
{
    public void UpdateTimerText(string minutes, string seconds);
    public void UpdateTimerProgress(float progress);
}
