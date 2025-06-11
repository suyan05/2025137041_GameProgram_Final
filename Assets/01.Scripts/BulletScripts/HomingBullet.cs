using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    // źȯ�� �̵� �ӵ�
    public float moveSpeed = 5f;
    // ��ǥ�� �ٶ󺸵��� ȸ���ϴ� �ӵ� (��/��)
    public float rotateSpeed = 200f;
    // źȯ�� ���� (��)
    public float lifetime = 5f;

    private Transform target;        // ������ ��� (�÷��̾�)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���� �ð��� ������ źȯ �ڵ� �ı�
        Destroy(gameObject, lifetime);

        // "Player" �±׸� ���� ������Ʈ�� ã�� ��ǥ�� ����
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void FixedUpdate()
    {
        // ���� ��ǥ�� ������ ���� �������� ���� �ӵ��� ����
        if (target == null)
        {
            rb.velocity = transform.up * moveSpeed;
            return;
        }

        // ���� ��ġ���� ��ǥ������ ���� ��� (����ȭ�� ����)
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        // ��ǥ ���� ���� ��� (Sprite�� ������ �ٶ󺸵��� -90���� ����)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // ���� ȸ�� ������ ��ǥ ������ ������ ����ϴ�.
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);
        // ȸ���� transform.up �������� �̵�
        rb.velocity = transform.up * moveSpeed;
    }
}