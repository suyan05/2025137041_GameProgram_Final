using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ReflectingBullet : MonoBehaviour
{
    public float speed = 6f;           // 탄환 속도
    public int maxBounces = 3;         // 최대 반사 횟수
    private int bounceCount = 0;       // 현재 반사 횟수

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    // 물리 충돌이 있을 때 호출 (Collider2D.isTrigger == false)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 대상이 벽 또는 Reflector 태그를 가지고 있는지 확인
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Reflector"))
        {
            // 첫 번째 컨택트 포인트의 노멀을 가져와 반사 벡터 계산
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 inVelocity = rb.velocity;
            Vector2 reflectDir = Vector2.Reflect(inVelocity.normalized, contact.normal);

            // 반사된 방향으로 속도 적용
            rb.velocity = reflectDir * speed;
            bounceCount++;

            // 최대 반사 횟수 초과 시 파괴
            if (bounceCount > maxBounces)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // 그 외 충돌(예: 플레이어, 보스) 시 즉시 파괴
            Destroy(gameObject);
        }
    }
}