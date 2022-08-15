using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu;

    protected void TurnOn()
    {
        _menu.alpha = 1;
        _menu.blocksRaycasts = true;
        _menu.interactable = true;
    }

    protected void TurnOff()
    {
        _menu.alpha = 0;
        _menu.blocksRaycasts = false;
        _menu.interactable = false;
    }
}