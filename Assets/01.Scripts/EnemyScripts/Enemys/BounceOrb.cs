using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BounceOrb : MonoBehaviour, IActivatablePattern
{
    public MonoBehaviour[] shooters;
    public float initialSpeed = 4f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randDir = Random.insideUnitCircle.normalized;
        rb.velocity = randDir * initialSpeed;
    }

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
}