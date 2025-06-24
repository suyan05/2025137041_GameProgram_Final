using UnityEngine;

public class HomingBullet : Bullet
{
    public float attractionRange = 5f;
    public float pullSpeed = 3f;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Update()
    {
        base.Update();
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= attractionRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * pullSpeed * Time.deltaTime);
        }
    }
}