using Sapper.Model;
using Sapper.View;
using UnityEngine;

namespace Sapper.Controller
{
    public class TimerController : MonoBehaviour
    {
        private TimerView _timerView;
        private Timer _timer;

        public void Init(TimerView timerView, Timer timer)
        {
            _timerView = timerView;
            _timer = timer;
        }

        private void Start()
        {
            _timer.Reset();
            _timerView.UpdateTimerText();
        }

        private void Update()
        {
            _timer.AddTime(UnityEngine.Time.deltaTime);
            _timerView.UpdateTimerText();
        }
    }
}