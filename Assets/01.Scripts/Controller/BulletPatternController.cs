using UnityEngine;

public class BulletPatternController : MonoBehaviour
{
    public MonoBehaviour[] shooters;  // 발사기 컴포넌트
    public float[] patternDurations;  // 각 패턴 지속 시간 (초)

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