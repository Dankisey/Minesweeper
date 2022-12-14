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
    [SerializeField] private MapViewFabric _mapViewFabric;
    [SerializeField] private MapController _mapController;
    [SerializeField] private RecordController _recordController;
    [SerializeField] private RecordView _recordView;
    [SerializeField] private SoundPlayer _soundPlayer;

    private GameStateObserver _gameStateObserver;
    private BombCounter _bombCounter;
    private Timer _timer;
    private Map _map;

    private void Awake()
    {
        CreateModels();
        InitViews();
    }

    private void CreateModels()
    {
        _map = new Map().Generate();
        _gameStateObserver = new(_map);
        _bombCounter = new(_gameStateObserver);
        _timer = new();
    }

    private void InitViews()
    {
        _soundPlayer.Init(_gameStateObserver);
        _mapViewFabric.Init(_map);
        _mapController.Init(_map);
        _recordView.Init(_recordController);
        _recordController.Init();
        _restartMenu.Init(_gameStateObserver, _timer);
        _bombCounterView.Init(_bombCounter);
        _timerController.Init(_timerView, _timer);
        _timerView.Init(_timer);
    }
}
