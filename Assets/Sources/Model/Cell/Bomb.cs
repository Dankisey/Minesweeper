using System;

namespace Sapper.Model
{
    public class Bomb : Cell
    {
        public Bomb(Vector2Int position) : base(position) { }

        public event Action<Bomb> Exploded;

        public void ForceExplosion()
        {
            Exploded?.Invoke(this);
        }

        public override Cell NotifyAboutNearBomb()
        {
            return this;
        }

        protected override Cell Open()
        {
            Exploded?.Invoke(this);
            return this;
        }
    }
}