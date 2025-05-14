using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject muzzleFlash;

    public Transform ironSight;
    public float zoomFOV = 30f;
    public float zoomSpeed = 8f;

    public Vector3 recoilAmount = new Vector3(-0.05f, 0, 0);
    public float recoilSpeed = 10f;
    public int maxAmmo = 20;
    private int currentAmmo;
    private bool isReloading = false;

    private Camera cam;
    private float normalFOV;
    private Vector3 normalCamPos;
    private Vector3 gunOriginalPos;

    void Start()
    {
        cam = Camera.main;
        normalFOV = cam.fieldOfView;
        normalCamPos = cam.transform.localPosition;
        currentAmmo = maxAmmo;
        GameUIManager.instance.UpdateAmmo(currentAmmo, maxAmmo);
        EquipGun1();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun1();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) EquipGun2();

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        bool isAiming = Input.GetMouseButton(1);

        float targetFOV = isAiming ? zoomFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        Vector3 targetPos = isAiming && ironSight != null
            ? cam.transform.InverseTransformPoint(ironSight.position)
            : normalCamPos;

        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, targetPos, Time.deltaTime * zoomSpeed);

        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && !isReloading)
        {
            DoGunShoot();
        }
    }

    void EquipGun1()
    {
        gun1.SetActive(true);
        gun2.SetActive(false);

        Transform scope = gun1.transform.Find("Gun/Iron Sight_Low");
        if (scope != null) ironSight = scope;

        gunOriginalPos = gun1.transform.localPosition;
    }

    void EquipGun2()
    {
        gun1.SetActive(false);
        gun2.SetActive(true);

        Transform scope = gun2.transform.Find("Gun2/Iron Sight_Low");
        if (scope != null) ironSight = scope;

        gunOriginalPos = gun2.transform.localPosition;
    }

    void DoGunShoot()
    {
        currentAmmo--;
        GameUIManager.instance.UpdateAmmo(currentAmmo, maxAmmo);

        if (bulletPrefab && bulletSpawn)
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        if (muzzleFlash && bulletSpawn)
        {
            var flash = Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation, bulletSpawn);
            Destroy(flash, 0.1f);
        }

        AudioManager.instance.PlayGunShot();

        GameObject activeGun = gun1.activeSelf ? gun1 : gun2;
        StopAllCoroutines();
        StartCoroutine(DoRecoil(activeGun.transform));
    }

    IEnumerator DoRecoil(Transform gun)
    {
        gun.localPosition += recoilAmount;
        yield return null;

        while (Vector3.Distance(gun.localPosition, gunOriginalPos) > 0.01f)
        {
            gun.localPosition = Vector3.Lerp(gun.localPosition, gunOriginalPos, Time.deltaTime * recoilSpeed);
            yield return null;
        }
    }

    IEnumerator Reload()
    {
        if (isReloading) yield break;
        isReloading = true;
        AudioManager.instance.PlayReload();
        yield return new WaitForSeconds(4f);
        currentAmmo = maxAmmo;
        GameUIManager.instance.UpdateAmmo(currentAmmo, maxAmmo);
        isReloading = false;
    }
}
