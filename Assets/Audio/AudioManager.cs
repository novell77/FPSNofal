using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource BulletSpawn;
    public AudioSource _walkSound;
    public AudioSource _runSoun;
    public AudioSource _jumpSound;
    public AudioSource Magazine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayGunShot()
    {
        BulletSpawn.Play();
    }

    public void PlayReload()
    {
        Magazine.Play();
    }

    public void PlayWalk()
    {
        if (!_walkSound.isPlaying)
        {
            _runSoun.Stop();
            _walkSound.Play();
        }
    }

    public void PlayRun()
    {
        if (!_runSoun.isPlaying)
        {
            _walkSound.Stop();
            _runSoun.Play();
        }
    }

    public void StopWalkRun()
    {
        _walkSound.Stop();
        _runSoun.Stop();
    }

    public void PlayJump()
    {
        _jumpSound.Play();
    }
}
