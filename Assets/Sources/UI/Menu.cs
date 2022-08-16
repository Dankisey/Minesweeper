using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu;

    public void TurnOn()
    {
        _menu.alpha = 1;
        _menu.blocksRaycasts = true;
        _menu.interactable = true;
    }

    public void TurnOff()
    {
        _menu.alpha = 0;
        _menu.blocksRaycasts = false;
        _menu.interactable = false;
    }
}