using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public int damage = 1;
    public float speed = 60f;
    public float lifetime = 5f;

    Rigidbody rb;
    bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;

        var zh = other.GetComponent<ZombieHealth>();
        if (zh != null)
        {
            zh.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
