using UnityEngine;

public class WaveBullet : Bullet
{
    public float waveAmplitude = 1f;     // 좌우 진폭
    public float waveFrequency = 2f;     // 좌우 흔들리는 속도

    private float waveTimer = 0f;
    private Vector2 forward;

    protected override void Start()
    {
        base.Start();
        forward = direction; // 발사 방향
    }

    protected override void Update()
    {
        waveTimer += Time.deltaTime;

        // 좌우로 흔들리는 오프셋 계산
        Vector2 right = new Vector2(-forward.y, forward.x); // 발사 방향 기준 직각 방향
        Vector2 waveOffset = right * Mathf.Sin(waveTimer * waveFrequency) * waveAmplitude;

        // 위치 이동
        Vector2 moveStep = forward * speed * Time.deltaTime;
        transform.position += (Vector3)(moveStep + waveOffset * Time.deltaTime);

        // 경계 체크 및 수명 유지
        base.Update();
    }
}