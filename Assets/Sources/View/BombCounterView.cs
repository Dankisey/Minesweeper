using Sapper.Model;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sapper.View
{
    public class BombCounterView : MonoBehaviour
    {
        [SerializeField] private Text _lastBombsLabel;
        [SerializeField] private Text _lastBombsAmount;
        [SerializeField] private float _valueChangeTime;

        private BombCounter _bombCounter;

        public void Init(BombCounter bombCounter)
        {
            if (_bombCounter != null)        
                _bombCounter.Changed -= OnScoreChanged;            

            _lastBombsLabel.text = "Bombs left:";
            _bombCounter = bombCounter;
            _bombCounter.Changed += OnScoreChanged;
        }

        private void OnScoreChanged(int amount)
        {
            _lastBombsAmount.DOText(amount.ToString(), _valueChangeTime, false, ScrambleMode.Numerals);
        }
    }
}