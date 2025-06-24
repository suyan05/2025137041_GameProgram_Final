using UnityEngine;

public class SplittingBullet : Bullet
{
    public GameObject splitBulletPrefab;     // �п��Ǿ� ���� ź�� ������
    public int splitCount = 5;               // �� ���� �п�����
    public float splitAngleRange = 180f;     // �� �� ������ ������
    public float splitSpeed = 5f;            // �п� ź���� �ӵ�

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(Split), lifetime);     // ���� �ð� �� �п�
    }

    private void Split()
    {
        float angleStep = splitAngleRange / (splitCount - 1);
        float startAngle = -splitAngleRange / 2f;

        for (int i = 0; i < splitCount; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = Instantiate(splitBulletPrefab, transform.position, Quaternion.identity);
            Bullet b = bullet.GetComponent<Bullet>();
            if (b != null)
            {
                b.Setup(dir, splitSpeed);
                b.bulletType = bulletType;
            }
        }

        Destroy(gameObject); // ���� ź���� ����
    }
}