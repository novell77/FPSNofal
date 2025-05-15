using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalEscapeTrigger : MonoBehaviour
{
    public Transform finalDoor;
    public Vector3 openOffset = new Vector3(0, 3f, 0);
    public float openSpeed = 2f;
    public int requiredKills = 4;

    public Animator helicopterAnimator;
    public string takeoffTrigger = "DoTakeoff";

    public Camera escapeCamera;
    public float endDelayAfterTouch = 0f;

    public GameObject endGameUI;
    public AudioClip endTheme;

    private AudioSource audioSource;
    private bool doorOpening = false;
    private bool planeTouched = false;
    private bool gameEnded = false;
    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;

    void Awake()
    {
        doorClosedPos = finalDoor.localPosition;
        doorOpenPos = doorClosedPos + openOffset;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = endTheme;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!doorOpening
            && KillCounter.instance != null
            && KillCounter.instance.killCount >= requiredKills)
        {
            doorOpening = true;
        }

        if (doorOpening)
        {
            finalDoor.localPosition = Vector3.Lerp(
                finalDoor.localPosition,
                doorOpenPos,
                Time.deltaTime * openSpeed
            );
        }

        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            if (audioSource.isPlaying) audioSource.Stop();
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!doorOpening || planeTouched) return;

        if (other.CompareTag("Player"))
        {
            planeTouched = true;
            escapeCamera.enabled = true;
            helicopterAnimator.SetTrigger(takeoffTrigger);
            Invoke(nameof(TriggerEndGame), endDelayAfterTouch);
        }
    }

    void TriggerEndGame()
    {
        Time.timeScale = 0f;
        if (endTheme != null) audioSource.Play();
        endGameUI.SetActive(true);
        gameEnded = true;
    }
}
