using UnityEngine;

public class OrbitingBullet : Bullet
{
    public Transform centerPoint;
    public float orbitRadius = 2f;
    public float orbitSpeed = 180f;
    public float angleOffset = 0f;

    protected override void Update()
    {
        // 중심점이 있으면 궤도 따라 움직이기
        if (centerPoint != null)
        {
            float angle = angleOffset + orbitSpeed * Time.time;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * orbitRadius;
            transform.position = centerPoint.position + (Vector3)offset;
        }
        else
        {
            base.Update();
        }
    }
}