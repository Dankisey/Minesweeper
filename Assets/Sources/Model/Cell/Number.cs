namespace Sapper.Model
{
    public class Number : Cell
    {
        public Number(int value, Vector2Int position) : base(position)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public void IncreaceValue()
        {
            Value++;
        }

        public override Cell NotifyAboutNearBomb()
        {
            IncreaceValue();
            return this;
        }
    }
}