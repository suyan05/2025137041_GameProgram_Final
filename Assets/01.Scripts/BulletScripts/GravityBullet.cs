using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    public Transform gravitySource;  // �߷��� �߽� (��: ����)
    public float gravityForce = 9.8f;  // �߷� ����
    public float lifetime = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;  // �⺻ ���� �߷� ��� ���� �� ����
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (gravitySource != null)
        {
            Vector2 direction = (gravitySource.position - transform.position).normalized;
            rb.AddForce(direction * gravityForce);
        }
    }
}