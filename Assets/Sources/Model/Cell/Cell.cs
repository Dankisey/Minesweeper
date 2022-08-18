using System;

namespace Sapper.Model
{
    public abstract class Cell
    {
        private bool _isFlagged;
        
        public Cell(Vector2Int position)
        {
            Position = position;
            _isFlagged = false;
            IsOpened = false;
        }
        public bool IsOpened { get; private set; }

        public event Action<bool> FlagStatusChanged;
        public event Action<Cell> Opened;

        public Vector2Int Position { get; protected set; }

        public Cell TryOpen(out bool isSuccess)
        {
            isSuccess = !_isFlagged;

            if(IsOpened)
            {
                isSuccess = false;
                return this;
            }

            if (_isFlagged)
                return this;
            else
                return Open();
        }

        public bool ChangeFlagStatus()
        {
            if (IsOpened)
                return false;

            _isFlagged = !_isFlagged;
            FlagStatusChanged?.Invoke(_isFlagged);

            return _isFlagged;
        }

        protected virtual Cell Open()
        {
            if (IsOpened == true)
                return this;

            Opened?.Invoke(this);
            IsOpened = true;
            return this;
        }

        public abstract Cell NotifyAboutNearBomb();
    }
}