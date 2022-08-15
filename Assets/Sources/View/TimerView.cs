using UnityEngine.UI;
using UnityEngine;
using System;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text _timer;

    private readonly Timer _time = new();

    private void Start()
    {
        _time.Reset();
        UpdateTimerText();
    }

    private void Update()
    {
        _time.AddTime(UnityEngine.Time.deltaTime);
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (_time.Seconds < 10)
            _timer.text = $"{_time.Minutes}:0{_time.Seconds}";
        else
            _timer.text = $"{_time.Minutes}:{_time.Seconds}";
    }
}

public class Timer
{
    private float _elapsedTime;

    public Timer()
    {
        _elapsedTime = 0;
    }

    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    public void AddTime(float timeToAdd)
    {
        if (timeToAdd < 0)       
            throw new ArgumentOutOfRangeException(nameof(timeToAdd));

        _elapsedTime += timeToAdd;
        SetFormatedTime();
    }

    public void Reset()
    {
        _elapsedTime = 0;
        SetFormatedTime();
    }

    private void SetFormatedTime()
    {
        Minutes = (int)Math.Floor(_elapsedTime / 60);
        Seconds = (int)Math.Floor(_elapsedTime % 60);
    }
}