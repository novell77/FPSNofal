using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Switching")]
    public GameObject gun;            // „Ã”„ «·”·«Õ
    public GameObject knife;          // „Ã”„ «·”ﬂÌ‰

    [Header("Shooting")]
    public GameObject bulletPrefab;   // prefab «·—’«’…
    public Transform bulletSpawn;     // ‰ﬁÿ… «‰ÿ·«ﬁ «·—’«’…
    public GameObject muzzleFlash;    // prefab ·„ƒÀ— «·Ê„Ì÷

    [Header("Aim Zoom")]
    public Transform ironSight;       // Transform ‰ﬁÿ… «·‹ iron sight
    public float zoomFOV = 30f;       // ﬁÌ„… «·‹ FOV ⁄‰œ «·“Ê„
    public float zoomSpeed = 8f;      // ”—⁄… «‰ ﬁ«· «·‹ FOV Ê«·„Ê÷⁄

    [Header("Aim Offset (Local)")]
    public Vector3 aimPositionOffset; // ≈“«Õ… «·ﬂ«„Ì—« «·„Õ·Ì… ⁄‰œ «·“Ê„

    [Header("Knife Settings")]
    public float knifeRange = 2f;     // „”«›… ÿ⁄‰… «·”ﬂÌ‰
    public int knifeDamage = 1;       // ÷—— «·ÿ⁄‰…

    private Animator weaponAnimator;
    private AudioSource audioSource;
    private Camera cam;
    private float normalFOV;
    private Vector3 normalCamPos;

    void Start()
    {
        // Ì· ﬁÿ Animator «·Ìœ/«·”·«Õ
        weaponAnimator = GetComponentInChildren<Animator>();
        // ’Ê  «·≈ÿ·«ﬁ ·Ê Õ«»
        audioSource = GetComponent<AudioSource>();
        // «·ﬂ«„Ì—« «·—∆Ì”Ì… »«·„‘Âœ
        cam = Camera.main;
        // Õ›Ÿ «·≈⁄œ«œ«  «·√’·Ì…
        normalFOV = cam.fieldOfView;
        normalCamPos = cam.transform.localPosition;
        // Ì»œ√ »«·”·«Õ
        EquipGun();
    }

    void Update()
    {
        // 1/2 ·· »œÌ· »Ì‰ «·”·«Õ Ê«·”ﬂÌ‰
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) EquipKnife();

        // «·“Ê„/«· ’ÊÌ» »«·“— «·√Ì„‰
        bool isAiming = Input.GetMouseButton(1);

        //  €ÌÌ— «·‹ FOV  œ—ÌÃÌ«
        float targetFOV = isAiming ? zoomFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        //  €ÌÌ— „Ê÷⁄ «·ﬂ«„Ì—« „Õ·Ì« Õ”» «·≈“«Õ… «·ÌœÊÌ…
        Vector3 targetPos = isAiming ? aimPositionOffset : normalCamPos;
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, targetPos, Time.deltaTime * zoomSpeed);

        // ≈ÿ·«ﬁ √Ê ÿ⁄‰… »«·“— «·√Ì”—
        if (Input.GetMouseButtonDown(0))
        {
            if (knife.activeSelf)
            {
                weaponAnimator?.SetTrigger("Stab");  // √‰Ì„Ì‘‰ «·ÿ⁄‰…
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
        // «‰‘«¡ «·—’«’…
        if (bulletPrefab && bulletSpawn)
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // „ƒÀ— «·‹ muzzle flash
        if (muzzleFlash && bulletSpawn)
        {
            var flash = Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation, bulletSpawn);
            Destroy(flash, 0.1f);
        }

        // ’Ê  «·≈ÿ·«ﬁ
        audioSource?.Play();
    }

    void DoKnifeDamage()
    {
        // ﬂ‘› Ê≈’«»… «·⁄œÊ
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out RaycastHit hit, knifeRange))
        {
            if (hit.collider.TryGetComponent<EnemyStateManager>(out var esm))
                esm.ChangeState(EnemyState.Dead);
        }
    }
}
