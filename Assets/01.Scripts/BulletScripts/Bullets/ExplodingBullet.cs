using UnityEngine;

public class ExplodingBullet : Bullet
{
    public GameObject explosionEffect;
    public float explosionRadius = 3f;

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(Explode), lifetime);
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}