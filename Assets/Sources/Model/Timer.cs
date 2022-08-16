using System;

namespace Sapper.Model
{
    public class Timer
    {
        private bool _isWorking;
        private float _elapsedTime;
        private int _minutes;
        private int _seconds;

        public Timer()
        {
            _isWorking = true;
            _elapsedTime = 0;
            _minutes = 0;
            _seconds = 0;
        }

        public Time Time => new(_minutes, _seconds);

        public void AddTime(float timeToAdd)
        {
            if (_isWorking == false)
                return;

            if (timeToAdd < 0)
                throw new ArgumentOutOfRangeException(nameof(timeToAdd));

            _elapsedTime += timeToAdd;
            SetFormatedTime();
        }

        public Time Stop()
        {
            _isWorking = false;
            return Time;
        }

        public void Reset()
        {
            _isWorking = true;
            _elapsedTime = 0;
            SetFormatedTime();
        }

        private void SetFormatedTime()
        {
            _minutes = (int)Math.Floor(_elapsedTime / 60);
            _seconds = (int)Math.Floor(_elapsedTime % 60);
        }
    }

    public struct Time
    {
        public readonly int Minutes;
        public readonly int Seconds;

        public Time(int minutes, int seconds)
        {
            Minutes = minutes;
            Seconds = seconds;
        }

        public string GetFormatedTime()
        {
            string formatedTime;

            if (Seconds < 10)
                formatedTime = $"{Minutes}:0{Seconds}";
            else
                formatedTime = $"{Minutes}:{Seconds}";

            return formatedTime;
        }
    }
}