using UnityEngine;

public class OrbitingBullet : Bullet
{
    [Header("회전 중심 (적의 Transform)")]
    public Transform center;

    [Header("회전 궤도 설정")]
    public float orbitRadius = 1.5f;        // 중심으로부터 거리
    public float orbitSpeed = 90f;          // 회전 속도 (도/초)
    public float radiusGrowthSpeed = 0f;    // 반지름 확장 속도 (선택 기능)

    [Header("초기 각도 (0~360도)")]
    public float startAngle = 0f;

    private float angle; // 현재 각도
    private float radius;

    protected override void Start()
    {
        if (center == null)
        {
            Debug.LogWarning("OrbitingBullet: 중심(center)이 설정되지 않았습니다!");
            enabled = false;
            return;
        }

        angle = startAngle;
        radius = orbitRadius;
    }

    protected override void Update()
    {
        if (center == null) return;

        // 각도 및 반지름 업데이트
        angle += orbitSpeed * Time.deltaTime;
        radius += radiusGrowthSpeed * Time.deltaTime;

        // 현재 위치 계산
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;
        transform.position = center.position + offset;
    }
}