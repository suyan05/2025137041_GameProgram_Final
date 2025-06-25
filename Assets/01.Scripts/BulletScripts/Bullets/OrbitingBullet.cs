using UnityEngine;

public class OrbitingBullet : Bullet
{
    [Header("ȸ�� �߽� (���� Transform)")]
    public Transform center;

    [Header("ȸ�� �˵� ����")]
    public float orbitRadius = 1.5f;        // �߽����κ��� �Ÿ�
    public float orbitSpeed = 90f;          // ȸ�� �ӵ� (��/��)
    public float radiusGrowthSpeed = 0f;    // ������ Ȯ�� �ӵ� (���� ���)

    [Header("�ʱ� ���� (0~360��)")]
    public float startAngle = 0f;

    private float angle; // ���� ����
    private float radius;

    protected override void Start()
    {
        if (center == null)
        {
            Debug.LogWarning("OrbitingBullet: �߽�(center)�� �������� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        angle = startAngle;
        radius = orbitRadius;
    }

    protected override void Update()
    {
        if (center == null) return;

        // ���� �� ������ ������Ʈ
        angle += orbitSpeed * Time.deltaTime;
        radius += radiusGrowthSpeed * Time.deltaTime;

        // ���� ��ġ ���
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
        transform.position = center.position + offset;
    }
}