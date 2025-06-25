using UnityEngine;

public class ZigDrone : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public float zigSpeed = 5f;
    public float zigAmplitude = 1f;
    public float zigFrequency = 3f;

    private Vector3 startPos;
    private float timeOffset;
    private bool isActive = false;

    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, 100f); // 패턴 다양화
        StartPattern();
    }

    void Update()
    {
        if (!isActive) return;

        float z = Time.time + timeOffset;
        Vector3 offset = Vector3.right * Mathf.Sin(z * zigFrequency) * zigAmplitude;
        transform.position += (Vector3.down * zigSpeed + offset) * Time.deltaTime;
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