using UnityEngine.UI;
using UnityEngine;

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
        _time.AddTime(Time.deltaTime);
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