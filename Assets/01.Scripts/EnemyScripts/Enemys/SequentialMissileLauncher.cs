using UnityEngine;

public class SequentialMissileLauncher : MonoBehaviour, IActivatablePattern
{
    public GameObject missilePrefab;              // ���� �̻��� ������
    public Transform[] firePoints;                // �߻� ��ġ��
    public float patternInterval = 3f;            // ��ü �߻� �ֱ�
    public float delayBetweenShots = 0.3f;        // �� ����Ʈ �� ����

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