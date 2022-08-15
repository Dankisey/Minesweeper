using Sapper.Model;
using UnityEngine.UI;
using UnityEngine;

namespace Sapper.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Text _timer;

        private Timer _time;

        public void Init(Timer timer)
        {
            _time = timer;
        }

        public void UpdateTimerText()
        {
            if (_time.Seconds < 10)
                _timer.text = $"{_time.Minutes}:0{_time.Seconds}";
            else
                _timer.text = $"{_time.Minutes}:{_time.Seconds}";
        }
    }
}