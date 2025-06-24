using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ReflectingBullet : Bullet
{
    public int maxBounces = 5;
    private int currentBounce = 0;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentBounce >= maxBounces)
        {
            Destroy(gameObject);
            return;
        }

        ContactPoint2D contact = collision.contacts[0];
        Vector2 incomingVelocity = rb.velocity;
        Vector2 normal = contact.normal;

        Vector2 reflectedVelocity = Vector2.Reflect(incomingVelocity, normal);
        rb.velocity = reflectedVelocity;

        currentBounce++;
    }

    protected override void Update()
    {
        base.Update(); // 생존시간 관리
    }
}