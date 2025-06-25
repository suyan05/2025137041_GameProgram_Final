using UnityEngine;

public class LineScrollSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform lineStart;
    public Transform lineEnd;
    public int enemyCount = 5;
    public float lineInterval = 7f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lineInterval)
        {
            timer = 0f;
            SpawnLine();
        }
    }

    void SpawnLine()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float t = (float)i / Mathf.Max(1, enemyCount - 1);
            Vector3 pos = Vector3.Lerp(lineStart.position, lineEnd.position, t);
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}