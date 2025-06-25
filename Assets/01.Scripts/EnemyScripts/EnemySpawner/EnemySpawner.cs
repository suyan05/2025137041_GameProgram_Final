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
    public SpawnAtPosition fixedSpawner; //�߰��� ���� ��ġ ������

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
            fixedSpawner //�߰�!
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

        Debug.Log($"[EnemySpawner] ���õ� ����: {activePattern.GetType().Name}");
    }

    void SwitchPattern()
    {
        if (activePattern != null)
            activePattern.enabled = false;

        PickRandomPattern();
    }
}