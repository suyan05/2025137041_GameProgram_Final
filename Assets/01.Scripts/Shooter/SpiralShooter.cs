using UnityEngine;

public class SpiralShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;      // �߻��� ź�� ������
    public float fireRate = 0.05f;       // �߻� ����
    public float spiralSpeed = 180f;     // ȸ�� �ӵ� (��/��)
    public float spiralGrowth = 0.2f;    // ���� �ݰ� ���� �ӵ�
    public float bulletSpeed = 6f;       // ź�� �ӵ�

    private float angle = 0f;
    private float fireTimer = 0f;
    private float spiralRadius = 0f;

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
            FireSpiralBullet();
        }

        angle += spiralSpeed * Time.deltaTime;
        spiralRadius += spiralGrowth * Time.deltaTime;
    }

    void FireSpiralBullet()
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.Setup(dir, bulletSpeed);
            b.bulletType = BulletType.EnemyBullet;
        }
    }
}