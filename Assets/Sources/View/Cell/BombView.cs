using Sapper.Model;

namespace Sapper.View
{
    public class BombView : CellView
    {
        public override void Init(Cell cell)
        {
            base.Init(cell);
            Data.SetBomb();
            (cell as Bomb).Exploded += OnExplode;
        }

        private void OnExplode(Bomb bomb)
        {
            bomb.Exploded -= OnExplode;
            ForceUnflag();
            ForceOpen();
        }
    }
}