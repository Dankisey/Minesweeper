using System;

namespace Sapper.Model
{
    public abstract class Cell
    {
        private bool _isOpenned;
        private bool _isFlagged;

        public Cell(Vector2Int position)
        {
            Position = position;
            _isFlagged = false;
            _isOpenned = false;
        }

        public event Action<bool> FlagStatusChanged;
        public event Action<Cell> Opened;

        public Vector2Int Position { get; protected set; }

        public Cell TryOpen(out bool isSuccess)
        {
            isSuccess = !_isFlagged;

            if(_isOpenned)
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
            if (_isOpenned)
                return false;

            _isFlagged = !_isFlagged;
            FlagStatusChanged?.Invoke(_isFlagged);

            return _isFlagged;
        }

        protected virtual Cell Open()
        {
            if (_isOpenned == true)
                return this;

            Opened?.Invoke(this);
            _isOpenned = true;
            return this;
        }

        public abstract Cell NotifyAboutNearBomb();
    }
}