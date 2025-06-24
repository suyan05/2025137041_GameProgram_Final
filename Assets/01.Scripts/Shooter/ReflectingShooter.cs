using UnityEngine;

public class ReflectingShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject bulletPrefab; // ReflectingBullet.cs ºÙÀº Åº¸· ÇÁ¸®ÆÕ
    public float interval = 1.2f;
    public float bulletSpeed = 5f;

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
            ReflectingBullet rb = b.GetComponent<ReflectingBullet>();
            if (rb != null)
                rb.Setup(transform.up, bulletSpeed);
        }
    }
}