using UnityEngine;

public enum BulletType
{
    PlayerBullet,  // 플레이어 탄환
    EnemyBullet    // 적 탄환
}


public class Bullet : MonoBehaviour
{
    public BulletType bulletType; // 탄환 타입 (플레이어 or 적)
    public float damage = 10f;    // 탄환 대미지
    public float lifetime = 3f;      // 탄환이 존재할 시간 (초)

    public Vector2 direction;
    public float speed = 5f;

    // 월드 경계 값 (필요에 따라 조정)
    public float boundary = 30f;


    // Setup을 통해 발사 방향과 속도를 설정 (방향은 항상 normalized)
    public void Setup(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
    }

    protected virtual void Start()
    {
        // 탄환 존재시간이 지난 후 파괴되도록 설정
        if (lifetime > 0)
        {
            Destroy(gameObject, lifetime);
        }

        if (bulletType == BulletType.EnemyBullet)
        {
            damage = 5f; // 적 탄환 대미지 조정
            direction = -direction; // 적 탄환은 플레이어를 향해 발사
        }
    }

    protected virtual void Update()
    {
        // 움직임 업데이트
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Bound Check: 지정한 경계를 넘어가면 파괴
        if (Mathf.Abs(transform.position.x) > boundary || Mathf.Abs(transform.position.y) > boundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.PlayerBullet)
        {
            //보스 파트 피격 처리
            if (collision.CompareTag("Boss"))
            {
                BossArm bossArm = collision.GetComponent<BossArm>();
                if (bossArm != null)
                    bossArm.TakeDamage(damage);

                BossCore bossCore = collision.GetComponent<BossCore>();
                if (bossCore != null)
                    bossCore.TakeDamage(damage);

                Destroy(gameObject);
            }
            //일반 적 피격 처리
            else if (collision.CompareTag("Enemy"))
            {
                EnemyStatus enemy = collision.GetComponent<EnemyStatus>();
                if (enemy != null)
                    enemy.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
        else if (bulletType == BulletType.EnemyBullet && collision.CompareTag("Player"))
        {
            //플레이어 피격 처리
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}