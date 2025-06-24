using UnityEngine;

public class RadialSpiralShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public float fireRate = 0.05f;
    public float spiralSpeed = 180f;       // 회전 각 속도 (도/초)
    public float radiusGrowthSpeed = 1.5f; // 점점 멀어지는 속도
    public float bulletSpeed = 0f;         // 발사한 탄막이 추가로 날아갈 경우 (0이면 고정됨)

    private float angle = 0f;
    private float radius = 0f;
    private float fireTimer = 0f;

    public bool isActive = false;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;


    void Update()
    {
        if (!isActive) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            FireBulletInSpiral();
        }

        angle += spiralSpeed * Time.deltaTime;
        radius += radiusGrowthSpeed * Time.deltaTime;
    }

    void FireBulletInSpiral()
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
        Vector2 spawnPos = (Vector2)transform.position + offset;

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            // bulletSpeed가 0이면 위치 고정, 0보다 크면 바깥으로 더 날아가게
            Vector2 dir = offset.normalized;
            b.Setup(dir, bulletSpeed);
            b.bulletType = BulletType.EnemyBullet;
        }
    }
}