namespace Sapper.Model
{
    public class Empty : Cell
    {
        public Empty(Vector2Int position) : base(position) { }

        public override Cell NotifyAboutNearBomb()
        {
            return new Number(1, Position);
        }
    }
}