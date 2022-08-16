using System;

namespace Sapper.Model
{
    public class Score
    {
        private readonly GameStateObserver _gameStateObserver;

        public Score(GameStateObserver gameStateObserver)
        {
            _gameStateObserver = gameStateObserver;
            _gameStateObserver.LastBombsAmountChanged += OnChanged;
            OnChanged();
        }

        public event Action<int> Changed;

        public void Destroy()
        {
            _gameStateObserver.LastBombsAmountChanged -= OnChanged;
        }

        private void OnChanged()
        {
            Changed?.Invoke(_gameStateObserver.LastBombs);
        }    
    }
}