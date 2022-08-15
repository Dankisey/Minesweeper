using System.Collections.Generic;
using System;

namespace Sapper.Model
{
    public class MapGenerator
    {
        private readonly Dictionary<Vector2Int, Bomb> _bombs;
        private readonly List<Vector2Int> _bombsPositions;
        private readonly Random _random;
        private readonly Map _map;

        private int _height;
        private int _width;

        private Cell[,] _currentMap;

        public MapGenerator(Map map)
        {
            _bombsPositions = new();
            _bombs = new();
            _random = new();
            _map = map;
        }

        public int CurrentBombsAmount { get; private set; }

        public Cell[,] Generate()
        {
            _height = _map.Height;
            _width = _map.Width;

            _currentMap = new Cell[_height, _width];

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _currentMap[i, j] = new Empty(new Vector2Int(j, i));
                }
            }

            SetCurrentBombsAmount();
            SetBombs();

            return _currentMap;
        }

        public BombsData GetBombsData()
        {
            BombsData bombsData = new(_bombs, _bombsPositions);

            return bombsData;
        }

        private void SetCurrentBombsAmount()
        {
            float bombsAmount = (float)Config.NormalDifficulty.MapHeight * (float)Config.NormalDifficulty.MapWidth / 100 * Config.NormalDifficulty.BombsPercentage;
            CurrentBombsAmount = (int)Math.Round(bombsAmount);
        }

        private void SetBombs()
        {
            for (int i = 0; i < CurrentBombsAmount; i++)
            {
                Vector2Int bombPlace = GetUniquePlace();

                Bomb bomb = new(bombPlace);
                _currentMap[bombPlace.Y, bombPlace.X] = bomb;

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
                    if (i < 0 || j < 0 || i >= _height || j >= _width)
                        continue;

                    _currentMap[i, j] = _currentMap[i, j].NotifyAboutNearBomb();
                }
            }
        }

        private Vector2Int GetUniquePlace()
        {
            Vector2Int newPlace;
            int x;
            int y;

            do
            {
                x = _random.Next(_width);
                y = _random.Next(_height);

                newPlace = new Vector2Int(x, y);

            } while (_bombsPositions.Contains(newPlace));

            return newPlace;
        }
    }

    public struct BombsData
    {
        public readonly Dictionary<Vector2Int, Bomb> Bombs;
        public readonly List<Vector2Int> BombsPositions;

        public BombsData(Dictionary<Vector2Int, Bomb> bombs, List<Vector2Int> bombsPositions)
        {
            BombsPositions = bombsPositions;
            Bombs = bombs;
        }
    }
}