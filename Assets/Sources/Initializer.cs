using Sapper.Controller;
using Sapper.Model;
using Sapper.View;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private TimerController _timerController;
    [SerializeField] private RestartMenu _restartMenu;
    [SerializeField] private Transform _mapParent;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private MapView _mapView;

    private GameStateObserver _gameStateObserver;
    private Score _score;
    private Timer _timer;
    private Map _map;

    public void Restart()
    {
        _score.Destroy();
        CreateModels();

        for (int i = 0; i < _mapParent.childCount; i++)
        {
            Transform cell = _mapParent.GetChild(i);
            Destroy(cell.gameObject);
        }

        _mapView.RestartGame();
        ReinstantiateViews();

        _restartMenu.TurnOff();
    }

    private void Awake()
    {
        _map = new Map();
        CreateModels();
        _mapView.Init(_map);
        ReinstantiateViews();
    }

    private void CreateModels()
    {
        _map = _map.Generate();
        _gameStateObserver = new(_map);
        _score = new(_gameStateObserver);
        _timer = new();
    }

    private void ReinstantiateViews()
    {
        _restartMenu.Init(_gameStateObserver, _timer);
        _scoreView.Init(_score);
        _timerController.Init(_timerView, _timer);
        _timerView.Init(_timer);
    }
}
