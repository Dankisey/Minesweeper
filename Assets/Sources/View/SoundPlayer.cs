using System.Collections;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bombOpenSound;
    [SerializeField] private AudioSource _cellOpenSound;
    [SerializeField] private AudioSource _flagSound;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private float _pauseBetweenSounds;
    [SerializeField] private float _delayBeforePlayingSound;

    private bool _isPlaying;
    private int _repeatsAmount = 0;

    public void PlayCellSound()
    {
        _repeatsAmount++;
        Invoke(nameof(PlayCellSoundMultiple), _delayBeforePlayingSound);
    }

    public void PlayBombSound()
    {
        _bombOpenSound.Play();
    }

    public void PlayFlagSound()
    {
        _flagSound.Play();
    }

    public void PlayWinSound()
    {
        _winSound.Play();
    }

    private void PlayCellSoundMultiple()
    {
        if (_isPlaying == true)        
            return;
        
        _isPlaying = true;
        StartCoroutine(PlayMulipleTimes(_cellOpenSound));
    }

    private IEnumerator PlayMulipleTimes(AudioSource audioSource)
    {
        WaitForSeconds pause = new(_pauseBetweenSounds);

        while (_repeatsAmount > 0)
        {
            audioSource.PlayOneShot(audioSource.clip);

            _repeatsAmount--;
            yield return pause;
        }

        _isPlaying = false;
    }
}
