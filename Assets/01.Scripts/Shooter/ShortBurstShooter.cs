using UnityEngine;

public class ShortBurstShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public float burstInterval = 1.5f;    // 전체 버스트 간격
    public int burstCount = 5;            // 한번에 발사할 탄 개수
    public float burstRate = 0.1f;        // 연속 발사 간격
    public float bulletSpeed = 6f;

    private bool isActive = false;
    private float burstTimer = 0f;
    private int currentBurst = 0;
    private float shotTimer = 0f;
    private bool isBursting = false;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;

    void Update()
    {
        if (!isActive) return;

        if (isBursting)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer >= burstRate)
            {
                shotTimer = 0f;
                FireBullet();
                currentBurst++;

                if (currentBurst >= burstCount)
                {
                    isBursting = false;
                    currentBurst = 0;
                    burstTimer = 0f;
                }
            }
        }
        else
        {
            burstTimer += Time.deltaTime;
            if (burstTimer >= burstInterval)
            {
                isBursting = true;
                shotTimer = 0f;
                currentBurst = 0;
            }
        }
    }

    void FireBullet()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            Vector2 dir = transform.up; // 또는 원하는 방향
            bullet.Setup(dir, bulletSpeed);
        }
    }
}