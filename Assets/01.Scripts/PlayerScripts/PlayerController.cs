using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("�ڵ� �߻� ����")]
    // �ڵ� �߻� ���� (�� ����)
    public float autoFireInterval = 0.25f;
    // �Ѿ��� �̵� �ӵ�
    public float bulletSpeed = 7f;

    [Header("��谪")]
    // �¿� ��谪 (Inspector���� ���� ����)
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
            // firePoint.rotation�� ����� �Ѿ��� �߻� ��ġ�� ȸ���� ���󰡵��� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                // firePoint.up �������� bulletSpeed ���� �Ѱܼ� �Ѿ��� �߻�
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