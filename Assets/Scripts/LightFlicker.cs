using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.5f;
    public float minDelay = 0.05f;
    public float maxDelay = 0.3f;
    public AudioSource flickerSound;
    public float soundChance = 0.4f;

    private Light lightSource;
    private bool isFlickering = true;

    void Start()
    {
        lightSource = GetComponent<Light>();
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (isFlickering)
        {
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);

            if (flickerSound != null && Random.value < soundChance)
            {
                flickerSound.Play();
            }

            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
