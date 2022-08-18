using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _bombOpenSound;
    [SerializeField] private AudioSource _cellOpenSound;
    [SerializeField] private AudioSource _flagSound;
    [SerializeField] private AudioSource _winSound;

    public void PlayCellSound()
    {
        _cellOpenSound.Play();
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
}
