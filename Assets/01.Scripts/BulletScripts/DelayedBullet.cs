using UnityEngine;

public class DelayedBullet : MonoBehaviour
{
    public float delayTime = 1f;  // ���� �ð�
    public float speed = 7f;      // �߻� �ӵ�
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
                // ���� ���¿��� �Ʒ������� �߻�
                if (rb != null)
                {
                    rb.velocity = Vector2.down * speed;
                }
                hasLaunched = true;
            }
        }
    }
}