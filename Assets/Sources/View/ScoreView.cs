using Sapper.Model;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sapper.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _lastBombsLabel;
        [SerializeField] private Text _lastBombsAmount;
        [SerializeField] private float _valueChangeTime;

        private Score _score;

        public void Init(Map map)
        {
            _lastBombsLabel.text = "Bombs left:";
            _score = new Score(map);
            _score.Changed += OnScoreChanged;
        }

        private void OnScoreChanged(int amount)
        {
            _lastBombsAmount.DOText(amount.ToString(), _valueChangeTime, false, ScrambleMode.Numerals);
        }
    }
}