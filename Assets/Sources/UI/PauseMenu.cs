using Sapper.Controller;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : Menu
{
    [SerializeField] private MapController _mapController;
    [SerializeField] private Button _pauseButton;

    public override void TurnOn()
    {
        base.TurnOn();
        Time.timeScale = 0;
        _mapController.DisableInput();
        _pauseButton.interactable = false;
    }

    public override void TurnOff()
    {
        base.TurnOff();
        Time.timeScale = 1;
        _mapController.EnableInput();
        _pauseButton.interactable = true;
    } 
}
