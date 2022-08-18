using Sapper.Model;
using UnityEngine;

namespace Sapper.Controller
{
    public class MapController : MonoBehaviour
    {
        private Map _map;

        public void Init(Map map)
        {
            _map = map;
        }

        public void DisableInput()
        {
            _map.DisableInputHandlers();
        }

        public void EnableInput()
        {
            _map.EnableInputHandlers();
        }
    }
}