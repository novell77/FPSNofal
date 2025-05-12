using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float sensitivity = 100f;

    [Header("Vertical Limits")]
    public float minPitch = -60f;
    public float maxPitch = 75f;

    [Header("References")]
    public Transform playerBody;    // ÇÓÍÈ åäÇ ßÇÆä Player ãä ÇáÜ Hierarchy

    float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ŞÑÇÁÉ ÍÑßÉ ÇáãÇæÓ
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // ÊÏæíÑ ÇáßÇãíÑÇ ÚãæÏíğÇ (Pitch)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // ÊÏæíÑ ÌÓã ÇááÇÚÈ ÃİŞíğÇ (Yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
