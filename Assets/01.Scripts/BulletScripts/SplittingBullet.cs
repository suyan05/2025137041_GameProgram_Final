using UnityEngine;

public class SplittingBullet : MonoBehaviour
{
    [Header("�п� ����")]
    public GameObject splitBulletPrefab;   // �п��Ǿ� ������ ������
    public int splitCount = 6;             // �п� ����
    public float splitDelay = 1.5f;        // �߻� �� �п����� �����ð�
    public bool splitOnCollision = false;  // �浹 �ÿ��� �п����� ����

    [Header("�п� źȯ �Ӽ�")]
    public float splitSpeed = 7f;          // �п� źȯ �߻� �ӵ�
    public float spreadAngle = 360f;       // �п� �� ������ �Ѱ���

    [Header("����")]
    public float lifetime = 5f;            // �� ���� (splitDelay ������ �ڵ� ���� ����)

    bool hasSplit = false;
    float timer;

    void Start()
    {
        timer = splitDelay;
        if (lifetime > 0f) Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // �ð� ��� �п�
        if (!hasSplit)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                DoSplit();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // �浹 ��� �п�
        if (splitOnCollision && !hasSplit)
        {
            DoSplit();
        }
    }

    void DoSplit()
    {
        hasSplit = true;

        // �п� �� �� spreadAngle ���� �ȿ��� splitCount�� �л�
        float angleStep = (splitCount > 1) ? spreadAngle / (splitCount - 1) : 0f;
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < splitCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            if (splitBulletPrefab != null)
            {
                GameObject b = Instantiate(splitBulletPrefab, transform.position, rot);
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.velocity = rot * Vector2.down * splitSpeed;
            }
        }

        Destroy(gameObject);
    }
}