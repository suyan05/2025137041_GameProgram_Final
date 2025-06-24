using UnityEngine;

public class WarpFlyer : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public float warpInterval = 2.5f;
    public Vector2 minPos = new Vector2(-4f, -4f);
    public Vector2 maxPos = new Vector2(4f, 4f);

    private float timer = 0f;
    private bool isActive = false;

    void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= warpInterval)
        {
            timer = 0f;
            Vector3 newPos = new Vector3(
                Random.Range(minPos.x, maxPos.x),
                Random.Range(minPos.y, maxPos.y),
                0f
            );
            transform.position = newPos;
        }
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