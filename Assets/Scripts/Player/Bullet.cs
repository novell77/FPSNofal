using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 60f;
    public float lifetime = 5f;

    Rigidbody rb;
    bool hasHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hasHit = false;
    }

    void Start()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        var zh = collision.collider.GetComponent<ZombieHealth>();
        if (zh != null)
        {
            zh.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
