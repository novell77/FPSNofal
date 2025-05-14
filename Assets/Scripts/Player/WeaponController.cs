using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject gun;
    public GameObject knife;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject muzzleFlash;

    public Transform ironSight;
    public float zoomFOV = 30f;
    public float zoomSpeed = 8f;

    public Vector3 aimPositionOffset;

    public float knifeRange = 2f;
    public int knifeDamage = 1;

    private Animator weaponAnimator;
    private AudioSource audioSource;
    private Camera cam;
    private float normalFOV;
    private Vector3 normalCamPos;

    void Start()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
        normalFOV = cam.fieldOfView;
        normalCamPos = cam.transform.localPosition;
        EquipGun();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) EquipKnife();

        bool isAiming = Input.GetMouseButton(1);

        float targetFOV = isAiming ? zoomFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        Vector3 targetPos = isAiming ? aimPositionOffset : normalCamPos;
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, targetPos, Time.deltaTime * zoomSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            if (knife.activeSelf)
            {
                weaponAnimator?.SetTrigger("Stab");
                DoKnifeDamage();
            }
            else
            {
                DoGunShoot();
            }
        }
    }

    void EquipGun()
    {
        gun.SetActive(true);
        knife.SetActive(false);
    }

    void EquipKnife()
    {
        gun.SetActive(false);
        knife.SetActive(true);
    }

    void DoGunShoot()
    {
        if (bulletPrefab && bulletSpawn)
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        if (muzzleFlash && bulletSpawn)
        {
            var flash = Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation, bulletSpawn);
            Destroy(flash, 0.1f);
        }

        audioSource?.Play();
    }

    void DoKnifeDamage()
    {
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out RaycastHit hit, knifeRange))
        {
            if (hit.collider.TryGetComponent<ZombieHealth>(out var zombieHealth))
            {
                zombieHealth.TakeDamage(knifeDamage);
            }
        }
    }
}
