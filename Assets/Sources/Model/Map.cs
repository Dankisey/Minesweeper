using System.Collections.Generic;
using System;

namespace Sapper.Model
{
    public class Map
    {
        private readonly Dictionary<Vector2Int, Bomb> _bombs;
        private readonly List<ClickHandler> _inputHandlers;
        private readonly List<Vector2Int> _bombsPositions;
        private readonly HashSet<Cell> _openedEmptyCells;
        private readonly Cell[,] _map;
        private readonly Random _random;

        public Map()
        {
            Height = Config.NormalDifficulty.MapHeight;
            Width = Config.NormalDifficulty.MapWidth;
            _map = new Cell[Height, Width];
            _openedEmptyCells = new();
            _bombsPositions = new();
            _inputHandlers = new();
            _bombs = new();
            _random = new();          
        }

        public event Action<bool> FlagStatusChanged;
        public event Action CellOpened;
        public event Action BombOpened;       

        public int BombsAmount { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Map Generate()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _map[i, j] = new Empty(new Vector2Int(j, i));
                }               
            }

            SetCurrentBombsAmount();
            SetBombs();

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
                inputHandler.Clicked -= TryOpenCell;
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

        private void SetBombs()
        {
            for (int i = 0; i < BombsAmount; i++)
            {
                Vector2Int bombPlace = GetUniquePlace();

                Bomb bomb = new(bombPlace);
                _map[bombPlace.Y, bombPlace.X] = bomb;
                bomb.Exploded += OnExplode;

                _bombsPositions.Add(bombPlace);
                _bombs.Add(bombPlace, bomb);

                NotifyNearCells(bombPlace);
            }
        }

        private void NotifyNearCells(Vector2Int bombPlace)
        {
            for (int i = bombPlace.Y - 1; i <= bombPlace.Y + 1; i++)
            {
                for (int j = bombPlace.X - 1; j <= bombPlace.X + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= Height || j >= Width)
                        continue;

                    _map[i,j] = _map[i, j].NotifyAboutNearBomb();
                }
            }
        }

        private void OnExplode(Bomb bomb)
        {
            bomb.Exploded -= OnExplode;

            foreach (var position in _bombsPositions)
            {
                _bombs[position].Exploded -= OnExplode;
                _bombs[position].ForceExplosion();
            }

            RemoveInputHandlers();
        }

        private Vector2Int GetUniquePlace()
        {
            Vector2Int newPlace;
            int x;
            int y;

            do
            {
                x = _random.Next(Width);
                y = _random.Next(Height);

                newPlace = new Vector2Int(x, y);

            } while (_bombsPositions.Contains(newPlace));

            return newPlace;
        }

        private void SetCurrentBombsAmount()
        {
            float bombsAmount = (float)Config.NormalDifficulty.MapHeight * (float)Config.NormalDifficulty.MapWidth / 100 * Config.NormalDifficulty.BombsPercentage;
            BombsAmount = (int)Math.Round(bombsAmount);
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