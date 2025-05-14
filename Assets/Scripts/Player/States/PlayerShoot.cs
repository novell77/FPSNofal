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

    public float reloadDuration = 2f; // مدة التعشيق
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        // ما يطلق إذا كان يعشق
        if (isReloading) return;

        if (Input.GetButtonDown("Fire1") && shootTimer >= shootRate)
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("ما فيه رصاص!");
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

        currentAmmo--;
        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("يعشق...");

        yield return new WaitForSeconds(reloadDuration); // ينتظر وقت التعشيق

        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        isReloading = false;
        Debug.Log("انتهى التعشيق!");
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo.ToString();
        }
    }
}
