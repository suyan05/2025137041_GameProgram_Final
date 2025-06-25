using UnityEngine;

public class MissileShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject missilePrefab;
    public float interval = 3f;

    private float timer;
    private bool isActive = false;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            FireMissile();
        }
    }

    void FireMissile()
    {
        Instantiate(missilePrefab, transform.position, transform.rotation);
    }
}