using UnityEngine;

public class BulletPatternController : MonoBehaviour
{
    public MonoBehaviour[] shooters;  // �߻�� ������Ʈ
    public float[] patternDurations;  // �� ���� ���� �ð� (��)

    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        ActivatePattern(currentIndex);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= patternDurations[currentIndex])
        {
            DeactivatePattern(currentIndex);
            currentIndex = (currentIndex + 1) % shooters.Length;
            ActivatePattern(currentIndex);
            timer = 0f;
        }
    }

    void ActivatePattern(int index)
    {
        if (shooters[index] is IActivatablePattern activatable)
            activatable.StartPattern();
    }

    void DeactivatePattern(int index)
    {
        if (shooters[index] is IActivatablePattern activatable)
            activatable.StopPattern();
    }
}