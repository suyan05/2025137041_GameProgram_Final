using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnZones;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;

            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Transform zone = spawnZones[Random.Range(0, spawnZones.Length)];
            Vector3 randomPos = zone.position + new Vector3(Random.Range(-2f, 2f), 0f, 0f);

            Instantiate(prefab, randomPos, Quaternion.identity);
        }
    }
}