using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 60f;
    public float lifetime = 10f;

    private Rigidbody rb;
    private bool hasHit = false;

    [System.Obsolete]
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * speed;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Destroy(gameObject, lifetime);
    }

    [System.Obsolete]
    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // تمنع التكرار
        hasHit = true;

        // إذا صدم زومبي يخصم له
        if (collision.collider.TryGetComponent<ZombieHealth>(out var zombie))
        {
            zombie.TakeDamage(damage);
        }

        // توقيف الحركة الأمامية وتفعيل الجاذبية
        rb.velocity = Vector3.zero;
        rb.useGravity = true;

        // تفعيل الاحتكاك والفيزياء الطبيعية
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // تبقى في العالم وما تختفي
    }
}
