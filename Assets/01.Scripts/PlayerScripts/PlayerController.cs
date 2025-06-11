using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("자동 발사 설정")]
    // 자동 발사 간격 (초 단위)
    public float autoFireInterval = 0.25f;
    // 총알의 이동 속도
    public float bulletSpeed = 7f;

    [Header("경계값")]
    // 좌우 경계값 (Inspector에서 조절 가능)
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;


    private float autoFireTimer;

    void Start()
    {
        autoFireTimer = autoFireInterval;
    }

    void Update()
    {
        Move();
        AutoShoot();
        Wrap();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(h, v, 0).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void AutoShoot()
    {
        autoFireTimer -= Time.deltaTime;
        if (autoFireTimer <= 0f)
        {
            // firePoint.rotation을 사용해 총알이 발사 위치의 회전을 따라가도록 설정
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                // firePoint.up 방향으로 bulletSpeed 값을 넘겨서 총알을 발사
                bulletScript.Setup(firePoint.up, bulletSpeed);
            }
            autoFireTimer = autoFireInterval;
        }
    }

    void Wrap()
    {
        Vector3 pos = transform.position;
        if (pos.x > rightBoundary)
        {
            pos.x = leftBoundary;
        }
        else if (pos.x < leftBoundary)
        {
            pos.x = rightBoundary;
        }
        transform.position = pos;
    }

}