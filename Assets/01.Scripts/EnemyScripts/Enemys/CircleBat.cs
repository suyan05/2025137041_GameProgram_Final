using UnityEngine;

public class CircleBat : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public float orbitRadius = 2f;
    public float orbitSpeed = 90f;

    private Vector3 center;
    private float angle;
    private bool isActive = false;

    void Start()
    {
        center = transform.position;
        angle = Random.Range(0f, 360f);
    }

    void Update()
    {
        if (!isActive) return;

        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * orbitRadius;
        transform.position = center + offset;
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