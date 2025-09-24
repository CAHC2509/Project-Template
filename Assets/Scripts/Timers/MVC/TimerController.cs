using UnityEngine;
using System.Collections.Generic;
using MEC;

public class TimerController : ControllerBase
{
    [SerializeField] private float timerDuration = 70f;
    [SerializeField] private float timerTickRate = 1f;

    private ITimerView view;
    private TimerBase timer;
    private CoroutineHandle timerCoroutine;

    private void Awake()
    {
        view = GetComponentInChildren<ITimerView>();
        timer = new CountUpTimer(timerDuration, timerTickRate);
    }

    private void Start()
    {
        Initialize();
    }

    private void OnApplicationQuit()
    {
        Conclude();
    }

    public override void Initialize()
    {
        base.Initialize();

        timerCoroutine = Timing.RunCoroutine(timer.TimerCoroutine());
        UpdateTimerView();
    }

    public override void Conclude()
    {
        base.Conclude();

        if (timerCoroutine != null)
            Timing.KillCoroutines(timerCoroutine);
    }

    protected override void AddListeners()
    {
        timer.OnTick += UpdateTimerView;
        timer.OnComplete += UpdateTimerView;
    }

    protected override void RemoveListeners()
    {
        timer.OnTick -= UpdateTimerView;
        timer.OnComplete -= UpdateTimerView;
    }

    private void UpdateTimerView()
    {
        int totalSeconds = Mathf.RoundToInt(timer.CurrentTime);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        view.UpdateTimerText(minutes.ToString("00"), seconds.ToString("00"));
        view.UpdateTimerProgress(timer.Progress);
    }
}
