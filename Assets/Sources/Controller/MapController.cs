using Sapper.Model;
using UnityEngine;

namespace Sapper.Controller
{
    public class MapController : MonoBehaviour
    {
        private Map _map;
        private bool _controlsAreActive;

        public void Init(Map map)
        {
            _map = map;
            _controlsAreActive = true;
        }

        public void DisableInput()
        {
            if (_controlsAreActive == false)
                return;

            _map.DisableInputHandlers();
            _controlsAreActive = false;
        }

        public void EnableInput()
        {
            if (_controlsAreActive)
                return;
            
            _map.EnableInputHandlers();
            _controlsAreActive = true;
        }
    }
}