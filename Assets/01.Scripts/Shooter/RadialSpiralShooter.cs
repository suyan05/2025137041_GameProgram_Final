using UnityEngine;

public class RadialSpiralShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public float fireRate = 0.05f;
    public float spiralSpeed = 180f;       // ȸ�� �� �ӵ� (��/��)
    public float radiusGrowthSpeed = 1.5f; // ���� �־����� �ӵ�
    public float bulletSpeed = 0f;         // �߻��� ź���� �߰��� ���ư� ��� (0�̸� ������)

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
            // bulletSpeed�� 0�̸� ��ġ ����, 0���� ũ�� �ٱ����� �� ���ư���
            Vector2 dir = offset.normalized;
            b.Setup(dir, bulletSpeed);
            b.bulletType = BulletType.EnemyBullet;
        }
    }
}