using UnityEngine;

public class HomingMissileWithLaunch : Bullet
{
    public float launchDuration = 0.5f;     // 수직 발사 시간
    public float homingTurnSpeed = 180f;    // 추적 회전 속도
    public float maxSpeed = 8f;
    public float acceleration = 3f;

    private float currentSpeed = 0f;
    private float launchTimer = 0f;
    private bool isHoming = false;

    private Transform target;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindWithTag("Player")?.transform;
        launchTimer = launchDuration;
        currentSpeed = 4f; // 수직 상승 초기 속도
    }

    protected override void Update()
    {
        base.Update();

        if (!isHoming)
        {
            // 1단계: 수직 상승 (local up 기준)
            transform.position += transform.up * currentSpeed * Time.deltaTime;
            launchTimer -= Time.deltaTime;

            if (launchTimer <= 0f)
                isHoming = true;
        }
        else
        {
            if (target == null) return;

            // 2단계: 유도 시작
            Vector2 toTarget = (target.position - transform.position).normalized;
            float angle = Vector2.SignedAngle(transform.up, toTarget);
            float rotate = Mathf.Clamp(angle, -homingTurnSpeed * Time.deltaTime, homingTurnSpeed * Time.deltaTime);
            transform.Rotate(0, 0, rotate);

            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            transform.position += transform.up * currentSpeed * Time.deltaTime;
        }
    }
}