using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    // 탄환의 이동 속도
    public float moveSpeed = 5f;
    // 목표를 바라보도록 회전하는 속도 (도/초)
    public float rotateSpeed = 200f;
    // 탄환의 수명 (초)
    public float lifetime = 5f;

    private Transform target;        // 추적할 대상 (플레이어)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 일정 시간이 지나면 탄환 자동 파괴
        Destroy(gameObject, lifetime);

        // "Player" 태그를 가진 오브젝트를 찾아 목표로 지정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void FixedUpdate()
    {
        // 만약 목표가 없으면 현재 방향으로 일정 속도로 직진
        if (target == null)
        {
            rb.velocity = transform.up * moveSpeed;
            return;
        }

        // 현재 위치에서 목표까지의 방향 계산 (정규화된 벡터)
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        // 목표 방향 각도 계산 (Sprite가 위쪽을 바라보도록 -90도를 보정)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // 현재 회전 각도를 목표 각도로 서서히 맞춥니다.
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);
        // 회전한 transform.up 방향으로 이동
        rb.velocity = transform.up * moveSpeed;
    }
}