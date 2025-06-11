using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ReflectingBullet : MonoBehaviour
{
    public float speed = 6f;           // źȯ �ӵ�
    public int maxBounces = 3;         // �ִ� �ݻ� Ƚ��
    private int bounceCount = 0;       // ���� �ݻ� Ƚ��

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    // ���� �浹�� ���� �� ȣ�� (Collider2D.isTrigger == false)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 ����� �� �Ǵ� Reflector �±׸� ������ �ִ��� Ȯ��
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Reflector"))
        {
            // ù ��° ����Ʈ ����Ʈ�� ����� ������ �ݻ� ���� ���
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 inVelocity = rb.velocity;
            Vector2 reflectDir = Vector2.Reflect(inVelocity.normalized, contact.normal);

            // �ݻ�� �������� �ӵ� ����
            rb.velocity = reflectDir * speed;
            bounceCount++;

            // �ִ� �ݻ� Ƚ�� �ʰ� �� �ı�
            if (bounceCount > maxBounces)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // �� �� �浹(��: �÷��̾�, ����) �� ��� �ı�
            Destroy(gameObject);
        }
    }
}