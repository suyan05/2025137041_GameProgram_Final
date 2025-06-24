using UnityEngine;

public class SnailBot : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public float moveSpeed = 1f;
    private bool isActive = false;

    void Update()
    {
        if (!isActive) return;

        // 느리게 내려오기만 하는 구조
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
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