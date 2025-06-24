using UnityEngine;

public class SlowdownBullet : Bullet
{
    public float slowdownRate = 1f;

    protected override void Update()
    {
        base.Update();
        speed = Mathf.Max(0f, speed - slowdownRate * Time.deltaTime);
    }
}