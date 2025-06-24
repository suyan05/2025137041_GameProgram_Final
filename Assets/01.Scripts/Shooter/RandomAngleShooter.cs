using UnityEngine;

public class RandomAngleShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab;
    public float interval = 0.6f;
    public float bulletSpeed = 4f;

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
            float angle = Random.Range(0f, 360f);
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = b.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Setup(dir, bulletSpeed);
        }
    }
}