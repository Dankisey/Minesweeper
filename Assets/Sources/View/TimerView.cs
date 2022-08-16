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
            _time.text = _timer.Time.GetFormatedTime();
        }
    }
}