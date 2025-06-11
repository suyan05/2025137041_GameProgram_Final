using UnityEngine;

public class RotatingBullet : MonoBehaviour
{
    public float rotationSpeed = 180f;  // 초당 회전 각도 (도)
    public float lifetime = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}