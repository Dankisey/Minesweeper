using Sapper.Model;
using UnityEngine.UI;
using UnityEngine;

public class RestartMenu : Menu
{
    [SerializeField] private Text _record;
    [SerializeField] private Text _time;

    private GameStateObserver _gameStateObserver;
    private Timer _timer;

    public void Init(GameStateObserver gameStateObserver, Timer timer)
    { 
        _gameStateObserver = gameStateObserver;
        _gameStateObserver.Lose += OnLose;
        _gameStateObserver.Win += OnWin;
        _timer = timer;
    }

    private void OnLose()
    {
        Unsubscribe();
        _timer.Stop();
        _time.text = "Time: none";
        TurnOn();
    }

    private void OnWin()
    {
        Unsubscribe();
        Sapper.Model.Time finalTime = _timer.Stop(); 
        _time.text = $"Time: {finalTime.GetFormatedTime()}";
        TurnOn();
    }

    private void Unsubscribe()
    {
        _gameStateObserver.Lose -= OnLose;
        _gameStateObserver.Win -= OnWin;
    }

    private void Awake()
    {
        TurnOff();
    }
}