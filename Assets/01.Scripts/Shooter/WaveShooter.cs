using UnityEngine;

public class WaveShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public float interval = 0.2f;
    public float bulletSpeed = 6f;

    private bool isActive = false;
    private float timer = 0f;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;

            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = b.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Setup(transform.up, bulletSpeed);
        }
    }
}