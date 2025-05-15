using UnityEngine;
using System.Collections;

public class SingleBayDoorController : MonoBehaviour
{
    public Transform door;
    public Vector3 openOffset = new Vector3(-3f, 0, 0);
    public float moveSpeed = 2f;
    public float delayBeforeClose = 5f;
    public AudioSource openSound;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Vector3 targetPosition;
    private Coroutine closeCoroutine;
    private bool playerInside = false;
    private bool hasPlayedOpenSound = false;

    void Start()
    {
        closedPosition = door.localPosition;
        openPosition = closedPosition + openOffset;
        targetPosition = closedPosition;
    }

    void Update()
    {
        door.localPosition = Vector3.Lerp(door.localPosition, targetPosition, Time.deltaTime * moveSpeed);

        if (targetPosition == openPosition && !hasPlayedOpenSound)
        {
            if (openSound != null)
            {
                openSound.Play();
                hasPlayedOpenSound = true;
            }
        }

        if (targetPosition == closedPosition)
        {
            hasPlayedOpenSound = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            targetPosition = openPosition;

            if (closeCoroutine != null)
            {
                StopCoroutine(closeCoroutine);
                closeCoroutine = null;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            closeCoroutine = StartCoroutine(DelayedClose());
        }
    }

    IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(delayBeforeClose);

        if (!playerInside)
        {
            targetPosition = closedPosition;
        }
    }
}
