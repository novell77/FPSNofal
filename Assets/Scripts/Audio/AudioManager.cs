
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunShot;
    public AudioSource footstep;
    public AudioSource enemyDeath;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayGunShot()
    {
        gunShot?.Play();
    }

    public void PlayFootstep()
    {
        footstep?.Play();
    }

    public void PlayEnemyDeath()
    {
        enemyDeath?.Play();
    }
}
