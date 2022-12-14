using Sapper.Controller;
using Sapper.Model;
using UnityEngine.UI;
using UnityEngine;

public class RestartMenu : Menu
{
    [SerializeField] private RecordController _recordController;
    [SerializeField] private float _secondsToEnable;
    [SerializeField] private Button _pauseButton;
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

    public override void TurnOn()
    {
        base.TurnOn();
        _pauseButton.interactable = false;
    }

    public override void TurnOff()
    {
        base.TurnOff();
        _pauseButton.interactable = true;
    }

    private void OnLose()
    {
        _timer.Stop();
        _time.text = "Time: none";
        Invoke(nameof(TurnOn), _secondsToEnable);
    }

    private void OnWin()
    {
        Sapper.Model.Time finalTime = _timer.Stop(); 
        _time.text = $"Time: {finalTime.GetFormatedTime()}";

        _recordController.TrySave(finalTime);

        Invoke(nameof(TurnOn), _secondsToEnable);
    }

    private void OnDisable()
    {
        _gameStateObserver.Lose -= OnLose;
        _gameStateObserver.Win -= OnWin;
    }

    private void Awake()
    {
        TurnOff();
    }
}