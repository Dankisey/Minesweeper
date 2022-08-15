using Sapper.Model;
using UnityEngine.UI;
using UnityEngine;

public class RestartMenu : Menu
{
    [SerializeField] private Text _record;
    [SerializeField] private Text _time;

    private GameStateObserver _gameStateObserver;

    public void Init(GameStateObserver gameStateObserver)
    {
        _gameStateObserver = gameStateObserver;
        _gameStateObserver.Lose += OnLose;
        _gameStateObserver.Win += OnWin;
    }

    private void OnLose()
    {
        TurnOn();
    }

    private void OnWin()
    {
        TurnOn();
    }

    private void Awake()
    {
        TurnOff();
    }
}