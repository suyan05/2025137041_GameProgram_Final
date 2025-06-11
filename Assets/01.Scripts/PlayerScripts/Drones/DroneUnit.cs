using UnityEngine;

public class DroneUnit : MonoBehaviour
{
    public Transform player;         // 플레이어의 Transform. 드론은 이 위치를 중심으로 도는 형태입니다.
    public float orbitRadius = 2f;     // 플레이어 주위를 도는 반지름
    public float orbitSpeed = 90f;     // 회전 속도 (초당 각도, 단위: 도)
    public float followSpeed = 5f;     // 플레이어를 향한 이동 보간 속도

    public float attackInterval = 2f;  // 공격 간격 (초)
    private float attackTimer;         // 공격 타이머

    public float bulletSpeed = 10f;    // 발사할 탄환의 속도
    public Transform firePoint;        // 탄환이 생성될 위치 (보통 드론의 자식 오브젝트)
    public GameObject droneBulletPrefab;  // 드론 탄환 프리팹 (개별 탄환 프리팹)

    // 드론이 배치될 오프셋 각도. DroneManager에서 설정해 전체 드론이 균등하게 배치되도록 합니다.
    public float offsetAngle;

    void Start()
    {
        attackTimer = attackInterval;
    }

    void Update()
    {
        if (player == null)
            return;

        // 드론의 현재 각도 계산: 초기 오프셋 + 시간에 따른 회전값
        float currentAngle = offsetAngle + orbitSpeed * Time.time;
        // cos, sin을 이용하여 플레이어 기준 원형 좌표를 계산합니다.
        Vector2 offset = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * orbitRadius;
        Vector3 targetPosition = player.position + new Vector3(offset.x, offset.y, 0f);

        // 드론이 목표 위치로 부드럽게 이동하도록 합니다.
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 공격 타이머 업데이트 및 간격마다 공격 함수 호출
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackInterval;
        }
    }

    // Attack() 함수에서 드론탄환 프리팹을 Instantiate하여 발사합니다.
    void Attack()
    {
        if (droneBulletPrefab != null && firePoint != null)
        {
            // 드론 탄환 프리팹을 firePoint 위치와 회전값으로 생성합니다.
            GameObject bullet = Instantiate(droneBulletPrefab, firePoint.position, firePoint.rotation);
            
            // 생성된 탄환에 Rigidbody2D가 있다면, firePoint의 오른쪽 방향(로컬 x축)으로 설정한 bulletSpeed 만큼의 속도를 부여합니다.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.up * bulletSpeed;
            }
            Debug.Log("드론 탄환 발사");
        }
        else
        {
            Debug.LogWarning("DroneBulletPrefab 또는 FirePoint가 설정되지 않았습니다.");
        }
    }
}