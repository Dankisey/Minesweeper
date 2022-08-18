using System.Collections;
using Sapper.Model;
using UnityEngine;
using System;

namespace Sapper.Controller
{
    public class ClickHandler : MonoBehaviour
    {
        private Cell _cell;

        public event Action<Cell> Clicked;
        public event Action<Cell> FlagStatusChanged;
         
        public void Init(Cell cell)
        {
            _cell = cell;
        }

        public void Click()
        {
            Clicked?.Invoke(_cell);
        }

        public void ChangeFlagStatus()
        {
            FlagStatusChanged?.Invoke(_cell);
        }
    }  
}