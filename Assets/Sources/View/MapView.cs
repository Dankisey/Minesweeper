using Sapper.Controller;
using Sapper.Model;
using Sapper.View;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private GameObject _cellTemplate;
    [SerializeField] private ScoreView _scoreView;
    
    private Map _mapModel;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _mapModel = new Map().Generate();
        _scoreView.Init(_mapModel);
        CreateCellsView();
    }

    private void CreateCellsView()
    {
        for (int i = 0; i < _mapModel.Height; i++)
        {
            for (int j = 0; j < _mapModel.Width; j++)
            {
                Cell cell = _mapModel.GetCellByIndex(i, j);
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
        _mapModel.AddInputHandler(inputHandler);
        cellView.Init(cell);
    }
}