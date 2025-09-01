using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerView : ViewBase, ITimerView
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerImage;

    public void UpdateTimerText(string minutes, string seconds)
    {
        timerText.text = $"{minutes}:{seconds}";
    }

    public void UpdateTimerProgress(float progress)
    {
        timerImage.fillAmount = progress;
    }
}
