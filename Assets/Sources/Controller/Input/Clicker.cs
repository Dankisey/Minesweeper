using UnityEngine.InputSystem;
using UnityEngine;

namespace Sapper.Controller
{
    public class Clicker : MonoBehaviour
    {
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Player.RightClick.performed += OnRightClick;
            _playerInput.Player.LeftClick.performed += OnLeftClick;
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void OnLeftClick(InputAction.CallbackContext context)
        {
            if (TryGetClickHandler(out ClickHandler clickHandler))
                clickHandler.Click();
        }

        private void OnRightClick(InputAction.CallbackContext context)
        {
            if (TryGetClickHandler(out ClickHandler clickHandler))
                clickHandler.ChangeFlagStatus();
        }

        private bool TryGetClickHandler(out ClickHandler clickHandler)
        {
            clickHandler = null;

            RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).origin, Vector3.forward);

            if (raycastHit.collider == null)
                return false;

            GameObject cellObject = raycastHit.collider.gameObject;

            if (cellObject.TryGetComponent<ClickHandler>(out clickHandler))
                return true;

            return false;
        }
    }
}