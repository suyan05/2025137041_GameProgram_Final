using UnityEngine;

public class SpiralFlower : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public GameObject explosionShooterPrefab;

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
        Instantiate(explosionShooterPrefab, transform.position, Quaternion.identity);
    }
}