using UnityEngine;

public class ExplodingShooter : MonoBehaviour, IActivatablePattern
{
    public GameObject explosionPrefab;
    public float interval = 3f;

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
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}