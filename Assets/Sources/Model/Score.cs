using System;

namespace Sapper.Model
{
    public class Score
    {
        private readonly int _cellsAmount;
        private readonly int _bombsAmount;
        private readonly Map _map;

        private int _flaggedCells;
        private int _openedCells;

        public Score(Map map)
        {
            _map = map;
            _bombsAmount = _map.BombsAmount;
            _cellsAmount = (_map.Height * _map.Width) - _bombsAmount;

            _map.FlagStatusChanged += OnFlagStatusChanged;
            _map.CellOpened += OnCellOpen;
            _map.BombOpened += OnBombOpen;

            _flaggedCells = 0;
            _openedCells = 0;
        }

        public void ResetScore()
        {
            Changed?.Invoke(_bombsAmount);
        }

        public event Action<int> Changed;
        public event Action GameOver;
        public event Action Win;

        private void OnFlagStatusChanged(bool addFlag)
        {
            if (addFlag)
                _flaggedCells++;
            else
                _flaggedCells--;

            Changed?.Invoke(_bombsAmount - _flaggedCells);
        }

        private void OnCellOpen()
        {
            _openedCells++;

            if (_openedCells >= _cellsAmount)
                DoWin();
        }

        private void OnBombOpen()
        {
            GameOver?.Invoke();
        }

        private void DoWin()
        {
            Win?.Invoke();
        }
    }
}