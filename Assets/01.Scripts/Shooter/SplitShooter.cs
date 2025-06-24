using UnityEngine;

public class SplitShooter : MonoBehaviour
{
    public GameObject spawnPrefab;
    public int count = 3;

    void OnDestroy()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.4f;
            Instantiate(spawnPrefab, transform.position + (Vector3)offset, Quaternion.identity);
        }
    }
}