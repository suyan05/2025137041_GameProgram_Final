using UnityEngine;

public class WaveBullet : MonoBehaviour
{
    public float baseSpeed = 5f;      // �Ʒ��� �����ϴ� �ӵ�
    public float frequency = 2f;      // ������ ���ļ� (�ʴ� ���� Ƚ��)
    public float amplitude = 2f;      // ������ ���� (���� �ִ� �̵��Ÿ�)
    public float lifetime = 5f;       // ź�� ����

    private Vector3 startPosition;
    private float timer = 0f;

    void Start()
    {
        startPosition = transform.position;
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        // �Ʒ��� ����
        float newY = startPosition.y - baseSpeed * timer;
        // ���� ������ (������)
        float offsetX = Mathf.Sin(timer * frequency * Mathf.PI * 2f) * amplitude;
        transform.position = new Vector3(startPosition.x + offsetX, newY, startPosition.z);
    }
}