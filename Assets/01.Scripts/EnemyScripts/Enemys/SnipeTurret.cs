using UnityEngine;

public class DualFireTurret : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject bulletPrefab;
    public float fireInterval = 2f;
    public float bulletSpeed = 6f;
    public float delayBetweenShots = 0.2f;

    private Transform target;
    private float fireTimer = 0f;
    private bool canShoot = false; //2초 대기 후 true로 전환

    void Start()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null)
            target = obj.transform;

        StartCoroutine(InitialDelay());
    }

    System.Collections.IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(2f);
        canShoot = true;
        fireTimer = fireInterval; // 바로 쏘기 위해 타이머도 초기화
    }

    void Update()
    {
        if (!canShoot || target == null || firePoints.Length < 2) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            StartCoroutine(FireWithDelay());
        }

        // 회전
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    System.Collections.IEnumerator FireWithDelay()
    {
        FireFromPoint(firePoints[0]);
        yield return new WaitForSeconds(delayBetweenShots);
        FireFromPoint(firePoints[1]);
    }

    void FireFromPoint(Transform point)
    {
        if (bulletPrefab == null || point == null) return;

        GameObject bullet = Instantiate(bulletPrefab, point.position, point.rotation);
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            Vector2 dir = (target.position - point.position).normalized;
            b.Setup(dir, -bulletSpeed);
        }
    }
}