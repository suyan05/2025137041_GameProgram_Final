using UnityEngine;

public class DelayedBullet : MonoBehaviour
{
    public float delayTime = 1f;  // 지연 시간
    public float speed = 7f;      // 발사 속도
    public float lifetime = 5f;

    private Rigidbody2D rb;
    private bool hasLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (!hasLaunched)
        {
            delayTime -= Time.deltaTime;
            if (delayTime <= 0f)
            {
                // 정지 상태에서 아래쪽으로 발사
                if (rb != null)
                {
                    rb.velocity = Vector2.down * speed;
                }
                hasLaunched = true;
            }
        }
    }
}