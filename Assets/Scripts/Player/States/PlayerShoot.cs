using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public int maxAmmo = 20;
    private int currentAmmo;
    public float shootRate = 0.2f;
    private float shootTimer;

    public GameObject bulletEffect;
    public Transform shootPoint;

    public TextMeshProUGUI ammoText;

    public float reloadDuration = 2f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (isReloading) return;

        if (Input.GetButtonDown("Fire1") && shootTimer >= shootRate)
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        shootTimer = 0f;

        if (bulletEffect != null && shootPoint != null)
        {
            Instantiate(bulletEffect, shootPoint.position, shootPoint.rotation);
        }

        AudioManager.instance.PlayGunShot();
        currentAmmo--;
        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        AudioManager.instance.PlayReload();
        yield return new WaitForSeconds(reloadDuration);
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        isReloading = false;
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }
}
