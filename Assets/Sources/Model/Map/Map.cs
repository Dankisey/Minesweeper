using Sapper.Controller;
using System.Collections.Generic;
using System;

namespace Sapper.Model
{
    public class Map
    {
        private readonly List<ClickHandler> _inputHandlers;      
        private readonly HashSet<Cell> _openedEmptyCells;
        private readonly MapGenerator _mapGenerator;

        private BombsData _bombsData;
        private Cell[,] _map;    

        public Map()
        {
            Height = Config.NormalDifficulty.MapHeight;
            Width = Config.NormalDifficulty.MapWidth;
            _mapGenerator = new(this);
            _openedEmptyCells = new();
            _inputHandlers = new();         
        }

        public event Action<bool> FlagStatusChanged;
        public event Action CellOpened;
        public event Action BombOpened;       

        public int BombsAmount { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
    
        public Map Generate()
        {
            _map = _mapGenerator.Generate();
            _bombsData = _mapGenerator.GetBombsData();
            BombsAmount = _mapGenerator.CurrentBombsAmount;

            foreach (var position in _bombsData.BombsPositions)         
                _bombsData.Bombs[position].Exploded += OnExplode;
            
            return this;
        }

        public Cell GetCellByIndex(int i, int j)
        {
            return _map[i, j];
        }

        public void AddInputHandler(ClickHandler inputHandler)
        {
            _inputHandlers.Add(inputHandler);
            inputHandler.Clicked += TryOpenCell;
            inputHandler.FlagStatusChanged += FlagCell;
        }

        private void RemoveInputHandlers()
        {
            foreach (var inputHandler in _inputHandlers)
            {
                inputHandler.Clicked -= TryOpenCell;
                inputHandler.FlagStatusChanged -= FlagCell;
            }            
        }

        private void FlagCell(Cell cell)
        {
            FlagStatusChanged?.Invoke(cell.ChangeFlagStatus());
        }

        private void TryOpenCell(Cell cell)
        {
            cell = cell.TryOpen(out bool isSuccess);                          

            if (isSuccess)
            {
                if (cell is Bomb)
                {
                    BombOpened?.Invoke();
                    return;
                }

                CellOpened?.Invoke();
            }
                

            if (cell is Empty)
            {
                _openedEmptyCells.Add(cell);
                TryOpenNearCells(cell.Position);
            }                        
        }

        private void TryOpenNearCells(Vector2Int centerCell)
        {         
            for (int i = centerCell.Y - 1; i <= centerCell.Y + 1; i++)
            {
                for (int j = centerCell.X - 1; j <= centerCell.X + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= Height || j >= Width)
                        continue;

                    if (_openedEmptyCells.Contains(_map[i,j]))                  
                        continue;

                    Cell cell = _map[i,j].TryOpen(out bool isSuccess);

                    if (isSuccess)
                        CellOpened?.Invoke();

                    if (cell is Empty empty)
                    {                                          
                        _openedEmptyCells.Add(empty);
                        TryOpenNearCells(empty.Position);
                    }
                }
            }
        }
      
        private void OnExplode(Bomb bomb)
        {
            bomb.Exploded -= OnExplode;

            foreach (var position in _bombsData.BombsPositions)
            {
                _bombsData.Bombs[position].Exploded -= OnExplode;
                _bombsData.Bombs[position].ForceExplosion();
            }

            RemoveInputHandlers();
        }            
    }

    public struct Vector2Int
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}