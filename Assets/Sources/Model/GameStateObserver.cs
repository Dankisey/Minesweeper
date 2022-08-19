using System;

namespace Sapper.Model
{
    public class GameStateObserver
    {
        private readonly int _cellsAmount;
        private readonly int _bombsAmount;
        private readonly Map _map;

        private int _openedCells;

        public GameStateObserver(Map map)
        {
            _map = map;
            _bombsAmount = _map.BombsAmount;
            _cellsAmount = (_map.Height * _map.Width) - _bombsAmount;

            _map.FlagStatusChanged += OnFlagStatusChanged;
            _map.CellOpened += OnCellOpen;
            _map.BombOpened += OnBombOpen;

            LastBombs = _bombsAmount;
            _openedCells = 0;
        }

        public event Action LastBombsAmountChanged;
        public event Action CellOpened;
        public event Action FlagSetted;
        public event Action Lose;
        public event Action Win;

        public int LastBombs { get; private set; }

        private void OnFlagStatusChanged(bool addFlag)
        {
            if (addFlag)
                LastBombs--;
            else
                LastBombs++;

            FlagSetted?.Invoke();
            LastBombsAmountChanged?.Invoke();
        }

        private void OnCellOpen()
        {
            CellOpened?.Invoke();
            _openedCells++;

            if (_openedCells >= _cellsAmount)
                DoWin();
        }

        private void OnBombOpen()
        {
            Unsubscribe();
            Lose?.Invoke();
        }

        private void DoWin()
        {
            Unsubscribe();
            Win?.Invoke();
        }

        private void Unsubscribe()
        {
            _map.FlagStatusChanged -= OnFlagStatusChanged;
            _map.CellOpened -= OnCellOpen;
            _map.BombOpened -= OnBombOpen;
        }
    }
}