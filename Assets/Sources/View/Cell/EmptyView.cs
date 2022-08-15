using Sapper.Model;

namespace Sapper.View
{
    public class EmptyView : CellView
    {
        public override void Init(Cell cell)
        {
            base.Init(cell);
            Data.SetNumber(0);
        }
    }
}