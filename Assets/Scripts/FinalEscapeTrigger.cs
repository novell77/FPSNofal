using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalEscapeTrigger : MonoBehaviour
{
    [Header("References")]
    public Animator helicopterAnimator;
    public Camera escapeCamera;
    public GameObject playerModel;
    public GameObject endUI;
    public AudioSource audioSource;
    public AudioClip endClip;

    [Header("Settings")]
    public float delayBeforeEnd = 5f;
    public string animationTrigger = "StartFly";

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // Disable player model
            if (playerModel != null)
                playerModel.SetActive(false);

            // Enable escape camera
            if (escapeCamera != null)
                escapeCamera.gameObject.SetActive(true);

            // Play helicopter animation
            if (helicopterAnimator != null)
                helicopterAnimator.SetTrigger(animationTrigger);

            // Play end sound
            if (endClip != null && audioSource != null)
                audioSource.PlayOneShot(endClip);

            // Show end UI after delay
            Invoke(nameof(ShowEndUI), delayBeforeEnd);
        }
    }

    void ShowEndUI()
    {
        if (endUI != null)
        {
            endUI.SetActive(true);
            Time.timeScale = 0f; // Freeze the game
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
