using UnityEngine;

public class DroneUnit : MonoBehaviour
{
    public Transform player;         // �÷��̾��� Transform. ����� �� ��ġ�� �߽����� ���� �����Դϴ�.
    public float orbitRadius = 2f;     // �÷��̾� ������ ���� ������
    public float orbitSpeed = 90f;     // ȸ�� �ӵ� (�ʴ� ����, ����: ��)
    public float followSpeed = 5f;     // �÷��̾ ���� �̵� ���� �ӵ�

    public float attackInterval = 2f;  // ���� ���� (��)
    private float attackTimer;         // ���� Ÿ�̸�

    public float bulletSpeed = 10f;    // �߻��� źȯ�� �ӵ�
    public Transform firePoint;        // źȯ�� ������ ��ġ (���� ����� �ڽ� ������Ʈ)
    public GameObject droneBulletPrefab;  // ��� źȯ ������ (���� źȯ ������)

    // ����� ��ġ�� ������ ����. DroneManager���� ������ ��ü ����� �յ��ϰ� ��ġ�ǵ��� �մϴ�.
    public float offsetAngle;

    void Start()
    {
        attackTimer = attackInterval;
    }

    void Update()
    {
        if (player == null)
            return;

        // ����� ���� ���� ���: �ʱ� ������ + �ð��� ���� ȸ����
        float currentAngle = offsetAngle + orbitSpeed * Time.time;
        // cos, sin�� �̿��Ͽ� �÷��̾� ���� ���� ��ǥ�� ����մϴ�.
        Vector2 offset = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * orbitRadius;
        Vector3 targetPosition = player.position + new Vector3(offset.x, offset.y, 0f);

        // ����� ��ǥ ��ġ�� �ε巴�� �̵��ϵ��� �մϴ�.
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // ���� Ÿ�̸� ������Ʈ �� ���ݸ��� ���� �Լ� ȣ��
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackInterval;
        }
    }

    // Attack() �Լ����� ���źȯ �������� Instantiate�Ͽ� �߻��մϴ�.
    void Attack()
    {
        if (droneBulletPrefab != null && firePoint != null)
        {
            // ��� źȯ �������� firePoint ��ġ�� ȸ�������� �����մϴ�.
            GameObject bullet = Instantiate(droneBulletPrefab, firePoint.position, firePoint.rotation);
            
            // ������ źȯ�� Rigidbody2D�� �ִٸ�, firePoint�� ������ ����(���� x��)���� ������ bulletSpeed ��ŭ�� �ӵ��� �ο��մϴ�.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.up * bulletSpeed;
            }
            Debug.Log("��� źȯ �߻�");
        }
        else
        {
            Debug.LogWarning("DroneBulletPrefab �Ǵ� FirePoint�� �������� �ʾҽ��ϴ�.");
        }
    }
}