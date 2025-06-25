using UnityEngine;

public class SequentialMissileLauncher : MonoBehaviour, IActivatablePattern
{
    public GameObject missilePrefab;              // 유도 미사일 프리팹
    public Transform[] firePoints;                // 발사 위치들
    public float patternInterval = 3f;            // 전체 발사 주기
    public float delayBetweenShots = 0.3f;        // 각 포인트 간 간격

    private float timer = 0f;
    private bool isActive = false;

    public void StartPattern() => isActive = true;
    public void StopPattern() => isActive = false;

    private void Start()
    {
        StartPattern();
    }

    void Update()
    {
        if (!isActive || firePoints.Length == 0 || missilePrefab == null) return;

        timer += Time.deltaTime;
        if (timer >= patternInterval)
        {
            timer = 0f;
            StartCoroutine(FireSequentially());
        }
    }

    System.Collections.IEnumerator FireSequentially()
    {
        foreach (Transform point in firePoints)
        {
            Instantiate(missilePrefab, point.position, point.rotation);
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }
}