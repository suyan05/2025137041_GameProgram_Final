using UnityEngine;

public class WaveBullet : Bullet
{
    public float waveAmplitude = 1f;     // �¿� ����
    public float waveFrequency = 2f;     // �¿� ��鸮�� �ӵ�

    private float waveTimer = 0f;
    private Vector2 forward;

    protected override void Start()
    {
        base.Start();
        forward = direction; // �߻� ����
    }

    protected override void Update()
    {
        waveTimer += Time.deltaTime;

        // �¿�� ��鸮�� ������ ���
        Vector2 right = new Vector2(-forward.y, forward.x); // �߻� ���� ���� ���� ����
        Vector2 waveOffset = right * Mathf.Sin(waveTimer * waveFrequency) * waveAmplitude;

        // ��ġ �̵�
        Vector2 moveStep = forward * speed * Time.deltaTime;
        transform.position += (Vector3)(moveStep + waveOffset * Time.deltaTime);

        // ��� üũ �� ���� ����
        base.Update();
    }
}