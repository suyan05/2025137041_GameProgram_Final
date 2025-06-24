using UnityEngine;

public class SplitPod : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public GameObject miniPodPrefab;
    public int spawnCount = 3;

    public void StartPattern()
    {
        foreach (var s in shooters)
            if (s is IActivatablePattern p) p.StartPattern();
    }

    public void StopPattern()
    {
        foreach (var s in shooters)
            if (s is IActivatablePattern p) p.StopPattern();
    }

    void OnDestroy()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.5f;
            Instantiate(miniPodPrefab, transform.position + (Vector3)offset, Quaternion.identity);
        }
    }
}