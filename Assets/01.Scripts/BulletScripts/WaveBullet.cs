using UnityEngine;

public class WaveBullet : MonoBehaviour
{
    public float baseSpeed = 5f;      // 아래로 진행하는 속도
    public float frequency = 2f;      // 사인파 주파수 (초당 진동 횟수)
    public float amplitude = 2f;      // 사인파 진폭 (수평 최대 이동거리)
    public float lifetime = 5f;       // 탄막 수명

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
        // 아래로 진행
        float newY = startPosition.y - baseSpeed * timer;
        // 수평 오프셋 (사인파)
        float offsetX = Mathf.Sin(timer * frequency * Mathf.PI * 2f) * amplitude;
        transform.position = new Vector3(startPosition.x + offsetX, newY, startPosition.z);
    }
}