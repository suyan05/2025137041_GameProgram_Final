using UnityEngine;

public class HomingMissileWithLaunch : Bullet
{
    public float launchDuration = 0.5f;     // ���� �߻� �ð�
    public float homingTurnSpeed = 180f;    // ���� ȸ�� �ӵ�
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
        currentSpeed = 4f; // ���� ��� �ʱ� �ӵ�
    }

    protected override void Update()
    {
        base.Update();

        if (!isHoming)
        {
            // 1�ܰ�: ���� ��� (local up ����)
            transform.position += transform.up * currentSpeed * Time.deltaTime;
            launchTimer -= Time.deltaTime;

            if (launchTimer <= 0f)
                isHoming = true;
        }
        else
        {
            if (target == null) return;

            // 2�ܰ�: ���� ����
            Vector2 toTarget = (target.position - transform.position).normalized;
            float angle = Vector2.SignedAngle(transform.up, toTarget);
            float rotate = Mathf.Clamp(angle, -homingTurnSpeed * Time.deltaTime, homingTurnSpeed * Time.deltaTime);
            transform.Rotate(0, 0, rotate);

            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            transform.position += transform.up * currentSpeed * Time.deltaTime;
        }
    }
}