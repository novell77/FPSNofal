using UnityEngine;

public class FinalEscapeTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCamera;
    public GameObject playerModel;
    public Camera escapeCamera;
    public Animator helicopterAnimator;
    public string animationName = "GoUp";
    public GameObject endUI;
    public float delayBeforeEnd = 5f;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (player != null) player.SetActive(false);
            if (playerModel != null) playerModel.SetActive(false);
            if (playerCamera != null) playerCamera.SetActive(false);

            if (escapeCamera != null) escapeCamera.enabled = true;

            if (helicopterAnimator != null)
                helicopterAnimator.Play(animationName);

            Invoke("ShowEndUI", delayBeforeEnd);
        }
    }

    void ShowEndUI()
    {
        if (endUI != null)
            endUI.SetActive(true);
    }
}
