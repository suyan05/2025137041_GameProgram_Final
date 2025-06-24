using UnityEngine;

public class ChaserBug : MonoBehaviour, IActivatablePattern
{
    public Transform player;
    public float moveSpeed = 3f;
    public MonoBehaviour[] shooters;

    private bool isActive = false;

    void Update()
    {
        if (!isActive || player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void StartPattern()
    {
        isActive = true;
        foreach (var s in shooters)
            if (s is IActivatablePattern p) p.StartPattern();
    }

    public void StopPattern()
    {
        isActive = false;
        foreach (var s in shooters)
            if (s is IActivatablePattern p) p.StopPattern();
    }
}