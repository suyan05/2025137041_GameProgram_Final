using UnityEngine;

public class AimedShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public Transform player;
    public float interval = 1.5f;
    public float bulletSpeed = 7f;

    private bool isActive = false;
    private float timer = 0f;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;

    void Update()
    {
        if (!isActive || player == null) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            Vector2 dir = (player.position - transform.position).normalized;

            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = b.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Setup(dir, bulletSpeed);
        }
    }
}