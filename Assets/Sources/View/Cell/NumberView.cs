using Sapper.Model;

public class NumberView : CellView
{
    public override void Init(Cell cell)
    {
        base.Init(cell);
        Data.SetNumber((CellModel as Number).Value);
    }
}
