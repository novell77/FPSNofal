using UnityEngine;

public class FinalEscapeTrigger : MonoBehaviour
{
    [Header("Door Settings")]
    [Tooltip("الباب الذي سيفتح عند تحقيق شرط القتل")]
    public Transform finalDoor;
    [Tooltip("المقدار الذي سيتحرك به الباب")]
    public Vector3 openOffset = new Vector3(0, 3f, 0);
    [Tooltip("سرعة فتح الباب")]
    public float openSpeed = 2f;

    [Header("Win Kill Requirement")]
    [Tooltip("عدد الزومبي الذي يجب قتله لفتح الباب")]
    public int requiredKills = 2;

    private Vector3 doorClosedPos;
    private Vector3 doorOpenPos;
    private bool doorOpening = false;

    void Start()
    {
        if (finalDoor != null)
        {
            doorClosedPos = finalDoor.localPosition;
            doorOpenPos = doorClosedPos + openOffset;
        }
    }

    void Update()
    {
        // تحقق مما إذا وصل اللاعب لعدد القتلات المطلوب
        if (!doorOpening
            && KillCounter.instance != null
            && KillCounter.instance.killCount >= requiredKills)
        {
            doorOpening = true;
            Debug.Log($"Reached {KillCounter.instance.killCount} kills → opening door");
        }

        // حرّك الباب تدريجياً نحو الحالة المفتوحة
        if (doorOpening && finalDoor != null)
        {
            finalDoor.localPosition = Vector3.Lerp(
                finalDoor.localPosition,
                doorOpenPos,
                Time.deltaTime * openSpeed
            );
        }
    }
}
