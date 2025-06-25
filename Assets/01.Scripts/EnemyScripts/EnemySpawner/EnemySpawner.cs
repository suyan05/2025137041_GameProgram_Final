using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnMode { SwitchPeriodically, AllAtOnce }
    public SpawnMode mode = SpawnMode.SwitchPeriodically;

    public float patternSwitchInterval = 10f;

    public TimerBasedSpawner timerSpawner;
    public WaveSpawner waveSpawner;
    public RandomSpawner randomSpawner;
    public LineScrollSpawner lineSpawner;
    public SpawnAtPosition fixedSpawner; //추가된 고정 위치 생성기

    private MonoBehaviour[] allPatterns;
    private MonoBehaviour activePattern;
    private float timer;

    void Start()
    {
        allPatterns = new MonoBehaviour[] {
            timerSpawner,
            waveSpawner,
            randomSpawner,
            lineSpawner,
            fixedSpawner //추가!
        };

        foreach (var p in allPatterns)
            p.enabled = false;

        if (mode == SpawnMode.AllAtOnce)
        {
            foreach (var p in allPatterns)
                p.enabled = true;
        }
        else
        {
            PickRandomPattern();
        }
    }

    void Update()
    {
        if (mode == SpawnMode.SwitchPeriodically)
        {
            timer += Time.deltaTime;
            if (timer >= patternSwitchInterval)
            {
                timer = 0f;
                SwitchPattern();
            }
        }
    }

    void PickRandomPattern()
    {
        int index = Random.Range(0, allPatterns.Length);
        activePattern = allPatterns[index];
        activePattern.enabled = true;

        Debug.Log($"[EnemySpawner] 선택된 패턴: {activePattern.GetType().Name}");
    }

    void SwitchPattern()
    {
        if (activePattern != null)
            activePattern.enabled = false;

        PickRandomPattern();
    }
}