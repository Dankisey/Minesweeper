using Sapper.Model;
using Sapper.View;
using UnityEngine;

namespace Sapper.Controller
{
    [RequireComponent(typeof(TimerView))]
    public class TimerController : MonoBehaviour
    {
        private readonly Timer _timer = new();

        private TimerView _timerView;

        private void Awake()
        {
            _timerView = GetComponent<TimerView>();
            _timerView.Init(_timer);
        }

        private void Start()
        {
            _timer.Reset();
            _timerView.UpdateTimerText();
        }

        private void Update()
        {
            _timer.AddTime(Time.deltaTime);
            _timerView.UpdateTimerText();
        }
    }
}