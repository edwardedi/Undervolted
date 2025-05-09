using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private int bulletDamage = 1;

    public bool fireBullet = false;
    private Transform target;

    private void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.x < 0)
            Destroy(gameObject);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target)
            return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed; //transform translate method
        bulletSpeed += 0.1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (fireBullet)
        {
            collision.gameObject.GetComponent<Health>().ApplyFireDamage(1, 3.2f, 1);
        }
        else
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
