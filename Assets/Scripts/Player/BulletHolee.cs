using UnityEngine;

public class BulletHolee : MonoBehaviour
{
    public GameObject bulletHolePrefab;

    private bool hasHit = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        // ✅ نمنع إنشاء الأثر إذا صدم زومبي
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(gameObject);
            return;
        }

        ContactPoint contact = collision.contacts[0];

        if (bulletHolePrefab != null)
        {
            Quaternion rot = Quaternion.LookRotation(contact.normal * -1f);
            GameObject hole = Instantiate(bulletHolePrefab, contact.point + contact.normal * 0.001f, rot);
            hole.transform.SetParent(collision.transform);
        }

        Destroy(gameObject);
    }
}
