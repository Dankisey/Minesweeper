using Sapper.Model;
using UnityEngine;

namespace Sapper.View
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _bombOpenSound;
        [SerializeField] private AudioSource _cellOpenSound;
        [SerializeField] private AudioSource _buttonSound;
        [SerializeField] private AudioSource _flagSound;
        [SerializeField] private AudioSource _winSound;

        private GameStateObserver _gameStateObserver;

        public void Init(GameStateObserver gameStateObserver)
        {
            _gameStateObserver = gameStateObserver;
            _gameStateObserver.FlagSetted+= PlayFlagSound;
            _gameStateObserver.CellOpened += PlayCellSound;
            _gameStateObserver.Win += PlayWinSound;
            _gameStateObserver.Lose += PlayBombSound;
        }

        public void PlayButtonSound()
        {
            _buttonSound.PlayOneShot(_buttonSound.clip);
        }

        private void PlayCellSound()
        {
            _cellOpenSound.Play();
        }

        private void PlayBombSound()
        {
            _bombOpenSound.Play();
        }

        private void PlayFlagSound()
        {
            _flagSound.Play();
        }

        private void PlayWinSound()
        {
            _winSound.Play();
        }

        private void OnDisable()
        {
            if (_gameStateObserver == null)
                return;

            _gameStateObserver.FlagSetted -= PlayFlagSound;
            _gameStateObserver.CellOpened -= PlayCellSound;
            _gameStateObserver.Win -= PlayWinSound;
            _gameStateObserver.Lose -= PlayBombSound;
        }
    }
}