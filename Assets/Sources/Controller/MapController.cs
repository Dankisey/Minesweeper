using Sapper.Model;
using Sapper.View;
using UnityEngine;

namespace Sapper.Controller
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private GameObject _cellTemplate;

        private Map _map;

        private void Start()
        {
            RestartGame();
        }

        public void Init(Map map)
        {
            _map = map;
        }

        public void RestartGame()
        {
            CreateCellsView();
        }

        private void CreateCellsView()
        {
            for (int i = 0; i < _map.Height; i++)
            {
                for (int j = 0; j < _map.Width; j++)
                {
                    Cell cell = _map.GetCellByIndex(i, j);
                    GameObject cellObject = Instantiate(_cellTemplate, new Vector3(cell.Position.X, cell.Position.Y), Quaternion.identity, transform);

                    if (cell is Empty)
                    {
                        EmptyView emptyView = cellObject.AddComponent<EmptyView>();
                        InitCellView(emptyView, cell);
                        continue;
                    }

                    if (cell is Number)
                    {
                        NumberView numberView = cellObject.AddComponent<NumberView>();
                        InitCellView(numberView, cell);
                        continue;
                    }

                    if (cell is Bomb)
                    {
                        BombView bombView = cellObject.AddComponent<BombView>();
                        InitCellView(bombView, cell);
                        continue;
                    }
                }
            }
        }

        private void InitCellView(CellView cellView, Cell cell)
        {
            ClickHandler inputHandler = cellView.gameObject.AddComponent<ClickHandler>();
            inputHandler.Init(cell);
            _map.AddInputHandler(inputHandler);
            cellView.Init(cell);
        }
    }
}