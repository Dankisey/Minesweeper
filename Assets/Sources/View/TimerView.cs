using Sapper.Model;
using UnityEngine.UI;
using UnityEngine;

namespace Sapper.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Text _time;

        private Timer _timer;

        public void Init(Timer timer)
        {
            _timer = timer;
        }

        public void UpdateTimerText()
        {
            if (_timer.Time.Seconds < 10)
                _time.text = $"{_timer.Time.Minutes}:0{_timer.Time.Seconds}";
            else
                _time.text = $"{_timer.Time.Minutes}:{_timer.Time.Seconds}";
        }
    }
}