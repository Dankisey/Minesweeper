using System;

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