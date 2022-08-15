using UnityEngine.UI;
using UnityEngine;
using System;

namespace Sapper.View
{
    public class CellViewData : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _bombSprite;
        [SerializeField] private Text _number;

        public void SetNumber(int value)
        {
            _bombSprite.gameObject.SetActive(false);
            _number.gameObject.SetActive(true);

            if (value < 0 || value > 8)
                throw new ArgumentOutOfRangeException(nameof(value));

            _number.text = value.ToString();

            if (value == 0)
                _number.gameObject.SetActive(false);
        }

        public void SetBomb()
        {
            _bombSprite.gameObject.SetActive(true);
            _number.gameObject.SetActive(false);
        }
    }
}