using UnityEngine;

public class SnipeTurret : MonoBehaviour, IActivatablePattern
{
    public Transform player;
    public MonoBehaviour shooter;
    public float rotationSpeed = 360f;

    private bool isActive = false;

    void Update()
    {
        if (!isActive || player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    public void StartPattern()
    {
        isActive = true;
        if (shooter is IActivatablePattern p) p.StartPattern();
    }

    public void StopPattern()
    {
        isActive = false;
        if (shooter is IActivatablePattern p) p.StopPattern();
    }
}