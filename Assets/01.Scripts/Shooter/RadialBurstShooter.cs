using UnityEngine;

public class RadialBurstShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public int bulletCount = 12;          // 발사할 탄 수
    public float bulletSpeed = 5f;
    public float interval = 2f;           // 몇 초마다 한 번씩 발사

    private float timer = 0f;
    private bool isActive = false;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            FireBurst();
        }
    }

    void FireBurst()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = b.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Setup(dir, bulletSpeed);
        }
    }
}