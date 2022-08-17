using Sapper.Controller;
using Sapper.Model;
using Sapper.View;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private TimerController _timerController;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private RestartMenu _restartMenu;
    [SerializeField] private BombCounterView _bombCounterView;
    [SerializeField] private MapController _mapView;
    [SerializeField] private RecordController _recordController;
    [SerializeField] private RecordView _recordView;

    private GameStateObserver _gameStateObserver;
    private BombCounter _bombCounter;
    private Timer _timer;
    private Map _map;

    private void Awake()
    {
        _map = new Map().Generate();
        _gameStateObserver = new(_map);
        _bombCounter = new(_gameStateObserver);
        _timer = new();
        _mapView.Init(_map);
        _recordView.Init(_recordController);
        _recordController.Init();
        _restartMenu.Init(_gameStateObserver, _timer);
        _bombCounterView.Init(_bombCounter);
        _timerController.Init(_timerView, _timer);
        _timerView.Init(_timer);
        
    }
}
