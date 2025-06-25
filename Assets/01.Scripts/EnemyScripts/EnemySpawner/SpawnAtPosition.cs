using UnityEngine;

public class SpawnAtPosition : MonoBehaviour
{
    public GameObject enemyPrefab;       // ������ �� ������
    public Transform[] spawnPositions;   // ���� ��ġ��

    public float spawnInterval = 5f;     // �� �ʸ��� ��������
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