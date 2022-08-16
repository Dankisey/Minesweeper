using UnityEngine.SceneManagement;
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
    [SerializeField] private MapController _mapView;

    private GameStateObserver _gameStateObserver;
    private Score _score;
    private Timer _timer;
    private Map _map;

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        _map = new Map().Generate();
        _gameStateObserver = new(_map);
        _score = new(_gameStateObserver);
        _timer = new();
        _mapView.Init(_map);
        _restartMenu.Init(_gameStateObserver, _timer);
        _scoreView.Init(_score);
        _timerController.Init(_timerView, _timer);
        _timerView.Init(_timer);
    }
}
