using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    public Transform gravitySource;  // 중력의 중심 (예: 보스)
    public float gravityForce = 9.8f;  // 중력 세기
    public float lifetime = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;  // 기본 내장 중력 대신 직접 힘 적용
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