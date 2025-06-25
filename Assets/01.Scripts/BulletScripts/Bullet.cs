using UnityEngine;

public enum BulletType
{
    PlayerBullet,  // �÷��̾� źȯ
    EnemyBullet    // �� źȯ
}


public class Bullet : MonoBehaviour
{
    public BulletType bulletType; // źȯ Ÿ�� (�÷��̾� or ��)
    public float damage = 10f;    // źȯ �����
    public float lifetime = 3f;      // źȯ�� ������ �ð� (��)

    public Vector2 direction;
    public float speed = 5f;

    // ���� ��� �� (�ʿ信 ���� ����)
    public float boundary = 30f;


    // Setup�� ���� �߻� ����� �ӵ��� ���� (������ �׻� normalized)
    public void Setup(Vector2 dir, float spd)
    {
        direction = dir.normalized;
        speed = spd;
    }

    protected virtual void Start()
    {
        // źȯ ����ð��� ���� �� �ı��ǵ��� ����
        if (lifetime > 0)
        {
            Destroy(gameObject, lifetime);
        }

        if (bulletType == BulletType.EnemyBullet)
        {
            damage = 5f; // �� źȯ ����� ����
            direction = -direction; // �� źȯ�� �÷��̾ ���� �߻�
        }
    }

    protected virtual void Update()
    {
        // ������ ������Ʈ
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // Bound Check: ������ ��踦 �Ѿ�� �ı�
        if (Mathf.Abs(transform.position.x) > boundary || Mathf.Abs(transform.position.y) > boundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.PlayerBullet)
        {
            //���� ��Ʈ �ǰ� ó��
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
            //�Ϲ� �� �ǰ� ó��
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
            //�÷��̾� �ǰ� ó��
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}