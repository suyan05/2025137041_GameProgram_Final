using UnityEngine;

public class SplittingBullet : MonoBehaviour
{
    [Header("분열 설정")]
    public GameObject splitBulletPrefab;   // 분열되어 생성될 프리팹
    public int splitCount = 6;             // 분열 개수
    public float splitDelay = 1.5f;        // 발사 후 분열까지 지연시간
    public bool splitOnCollision = false;  // 충돌 시에도 분열할지 여부

    [Header("분열 탄환 속성")]
    public float splitSpeed = 7f;          // 분열 탄환 발사 속도
    public float spreadAngle = 360f;       // 분열 시 퍼지는 총각도

    [Header("수명")]
    public float lifetime = 5f;            // 총 수명 (splitDelay 이전에 자동 삭제 방지)

    bool hasSplit = false;
    float timer;

    void Start()
    {
        timer = splitDelay;
        if (lifetime > 0f) Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 시간 기반 분열
        if (!hasSplit)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                DoSplit();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // 충돌 기반 분열
        if (splitOnCollision && !hasSplit)
        {
            DoSplit();
        }
    }

    void DoSplit()
    {
        hasSplit = true;

        // 분열 시 총 spreadAngle 범위 안에서 splitCount개 분산
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