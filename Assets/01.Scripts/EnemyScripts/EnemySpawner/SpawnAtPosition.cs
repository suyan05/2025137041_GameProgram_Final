using UnityEngine;

public class SpawnAtPosition : MonoBehaviour
{
    public GameObject enemyPrefab;       // 생성할 적 프리팹
    public Transform[] spawnPositions;   // 생성 위치들

    public float spawnInterval = 5f;     // 몇 초마다 생성할지
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemyAtFixedPositions();
        }
    }

    void SpawnEnemyAtFixedPositions()
    {
        foreach (Transform pos in spawnPositions)
        {
            Instantiate(enemyPrefab, pos.position, pos.rotation);
        }
    }
}