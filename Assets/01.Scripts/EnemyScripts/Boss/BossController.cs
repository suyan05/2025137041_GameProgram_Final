using UnityEngine;

public enum BossPattern
{
    Initial,    // �ʱ� ����
    Enhanced,   // ��ȭ ����
    Defensive,  // ��� ����
    Pressure,   // �й� ����
    Difficulty, // ���̵� ��� ����
    Special,    // Ư�� ���� (ü�� 30% ����)
    Critical,   // ���� ���� (ü�� 15% ����)
    Final       // ������ ���� (ü�� 5% ����)
}

public class BossController : MonoBehaviour, IDamageable
{
    [Header("Boss Parts")]
    public BossArm leftArm;
    public BossArm rightArm;
    public BossCore core;

    [Header("Firing Settings")]
    public BulletManager bulletManager;
    public Transform leftFirePosition;
    public Transform rightFirePosition;
    public Transform coreFirePosition;

    [Header("Pattern Settings")]
    public float patternInterval = 5f;
    private float patternTimer;
    private BossPattern currentPattern = BossPattern.Initial;
    private int difficultyLevel = 0;

    [Header("Boss Health")]
    public float maxHealth;      // ���� ��ü �ִ� ü�� (�� ������ ��)
    private float currentHealth; // ���� ��ü ���� ü��

    // �� �� ��� ��� ���� �� ������ �߻��ϱ� ���� ����
    private bool fireLeftNext = true;
    // �μ��� ������ ���� �÷���
    private bool leftArmNotified = false;
    private bool rightArmNotified = false;

    void Start()
    {
        // �� ������ �ִ� ü���� �ջ��ؼ� ���� maxHealth�� ����
        maxHealth = leftArm.maxHealth + rightArm.maxHealth + core.maxHealth;

        // ������ �ʱ� ü���� �� ������ ���� ü�� �ջ�
        currentHealth = leftArm.currentHealth + rightArm.currentHealth + core.currentHealth;

        patternTimer = patternInterval;

    }

    void Update()
    {
        patternTimer -= Time.deltaTime;
        if (patternTimer <= 0f)
        {
            ExecutePattern();
            patternTimer = patternInterval;
        }

        // ���� �� �μ��� ����
        if (leftArm.isDestroyed && !leftArmNotified)
        {
            Debug.Log("���� ���� �μ������ϴ�!");
            leftArmNotified = true;
            difficultyLevel++;
            // �߰� ȿ�� �� ���̵� ��� ó�� ����
        }

        // ������ �� �μ��� ����
        if (rightArm.isDestroyed && !rightArmNotified)
        {
            Debug.Log("������ ���� �μ������ϴ�!");
            rightArmNotified = true;
            difficultyLevel++;
        }

        // �� ���� ��� �μ����� �ھ ���ݰ����ϰ� ����
        if (leftArm.isDestroyed && rightArm.isDestroyed && !core.vulnerable)
        {
            core.vulnerable = true;
            Debug.Log("���� �ھ ���� �����������ϴ�!");
            difficultyLevel++;
        }

        // ü�� ��� ���� ��ȯ (����)
        float healthRatio = currentHealth / maxHealth;
        if (healthRatio <= 0.05f)
            currentPattern = BossPattern.Final;
        else if (healthRatio <= 0.15f)
            currentPattern = BossPattern.Critical;
        else if (healthRatio <= 0.3f)
            currentPattern = BossPattern.Special;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("���� ��ü ü��: " + currentHealth);
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���� ����!");
        // ���� ���� ���� ź�� ������ �ߴܵ˴ϴ�.
        // �߰��� BulletManager�� ���� ��鿡 �����ִ� ��� �� źȯ�� �����մϴ�.
        if (bulletManager != null)
        {
            bulletManager.ClearEnemyBullets();
        }
        // ���� ������Ʈ �ı�
        Destroy(gameObject);
    }



    // ���� ź�� ���� ���� �޼���
    void ExecutePattern()
    {
        switch (currentPattern)
        {
            case BossPattern.Initial:
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        // �����ư��� �߻�
                        if (fireLeftNext)
                        {
                            bulletManager.FireStraight(leftFirePosition.position, 5f + difficultyLevel);
                            bulletManager.FireSpread(leftFirePosition.position, 8 + difficultyLevel, 90f);
                        }
                        else
                        {
                            bulletManager.FireStraight(rightFirePosition.position, 5f + difficultyLevel);
                            bulletManager.FireSpread(rightFirePosition.position, 8 + difficultyLevel, 90f);
                        }
                        fireLeftNext = !fireLeftNext;
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireStraight(leftFirePosition.position, 5f + difficultyLevel);
                        bulletManager.FireSpread(leftFirePosition.position, 8 + difficultyLevel, 90f);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireStraight(rightFirePosition.position, 5f + difficultyLevel);
                        bulletManager.FireSpread(rightFirePosition.position, 8 + difficultyLevel, 90f);
                    }
                }
                break;


            case BossPattern.Enhanced:
                // ��ȭ ����: ź�� �ӵ� ������ ���� ź�� �߰�
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        if (fireLeftNext)
                        {
                            bulletManager.FireStraight(leftFirePosition.position, 8f + difficultyLevel);
                            bulletManager.FireHoming(leftFirePosition.position);
                        }
                        else
                        {
                            bulletManager.FireStraight(rightFirePosition.position, 8f + difficultyLevel);
                            bulletManager.FireHoming(rightFirePosition.position);
                        }
                        fireLeftNext = !fireLeftNext;
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireStraight(leftFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireHoming(leftFirePosition.position);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireStraight(rightFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireHoming(rightFirePosition.position);
                    }
                }
                break;

            case BossPattern.Defensive:
                // ��� ����: �ݻ� ź������ ���
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        if (fireLeftNext)
                        {
                            bulletManager.FireReflecting(leftFirePosition.position, 6f + difficultyLevel);
                        }
                        else
                        {
                            bulletManager.FireReflecting(rightFirePosition.position, 6f + difficultyLevel);
                        }
                        fireLeftNext = !fireLeftNext;
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireReflecting(leftFirePosition.position, 6f + difficultyLevel);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireReflecting(rightFirePosition.position, 6f + difficultyLevel);
                    }
                }
                break;

            case BossPattern.Pressure:
                // �й� ����: ���� ���� ź���� ȸ�� ź��
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        if (fireLeftNext)
                        {
                            bulletManager.FireStraight(leftFirePosition.position, 10f + difficultyLevel);
                            bulletManager.FireRotating(leftFirePosition.position, 180f);
                        }
                        else
                        {
                            bulletManager.FireStraight(rightFirePosition.position, 10f + difficultyLevel);
                            bulletManager.FireRotating(rightFirePosition.position, 180f);
                        }
                        fireLeftNext = !fireLeftNext;
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireStraight(leftFirePosition.position, 10f + difficultyLevel);
                        bulletManager.FireRotating(leftFirePosition.position, 180f);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireStraight(rightFirePosition.position, 10f + difficultyLevel);
                        bulletManager.FireRotating(rightFirePosition.position, 180f);
                    }
                }
                break;

            case BossPattern.Difficulty:
                // ���̵� ��� ����: ź�� �е� ���� �� �ĵ� ź�� ���
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        if (fireLeftNext)
                        {
                            bulletManager.FireWave(leftFirePosition.position, 5f + difficultyLevel, 2f, 3f);
                            bulletManager.FireSpread(leftFirePosition.position, 15 + difficultyLevel, 120f);
                        }
                        else
                        {
                            bulletManager.FireWave(rightFirePosition.position, 5f + difficultyLevel, 2f, 3f);
                            bulletManager.FireSpread(rightFirePosition.position, 15 + difficultyLevel, 120f);
                        }
                        fireLeftNext = !fireLeftNext;
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireWave(leftFirePosition.position, 5f + difficultyLevel, 2f, 3f);
                        bulletManager.FireSpread(leftFirePosition.position, 15 + difficultyLevel, 120f);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireWave(rightFirePosition.position, 5f + difficultyLevel, 2f, 3f);
                        bulletManager.FireSpread(rightFirePosition.position, 15 + difficultyLevel, 120f);
                    }
                }
                break;

            case BossPattern.Special:
                // Ư�� ����: �ð����� ź���� �߷� ź��
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        if (fireLeftNext)
                        {
                            bulletManager.FireDelayed(leftFirePosition.position, 1f, 7f + difficultyLevel);
                            bulletManager.FireGravity(leftFirePosition.position, transform, 3f);
                        }
                        else
                        {
                            bulletManager.FireDelayed(rightFirePosition.position, 1f, 7f + difficultyLevel);
                            bulletManager.FireGravity(rightFirePosition.position, transform, 3f);
                        }
                        fireLeftNext = !fireLeftNext;
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireDelayed(leftFirePosition.position, 1f, 7f + difficultyLevel);
                        bulletManager.FireGravity(leftFirePosition.position, transform, 3f);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireDelayed(rightFirePosition.position, 1f, 7f + difficultyLevel);
                        bulletManager.FireGravity(rightFirePosition.position, transform, 3f);
                    }
                }
                break;

            case BossPattern.Critical:
                // ���� ����: ��� ź�� ����
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        // ���� ���� ź���� ���̵� �ݿ�
                        bulletManager.FireStraight(leftFirePosition.position, 12f + difficultyLevel);
                        bulletManager.FireSpread(leftFirePosition.position, 10 + difficultyLevel, 360f);
                        bulletManager.FireHoming(leftFirePosition.position);
                        bulletManager.FireReflecting(leftFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireRotating(leftFirePosition.position, 240f);

                        // ������ ���� ź���� ���̵� �ݿ�
                        bulletManager.FireStraight(rightFirePosition.position, 12f + difficultyLevel);
                        bulletManager.FireSpread(rightFirePosition.position, 10 + difficultyLevel, 360f);
                        bulletManager.FireHoming(rightFirePosition.position);
                        bulletManager.FireReflecting(rightFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireRotating(rightFirePosition.position, 240f);
                    }
                    else if (!leftArm.isDestroyed)
                    {
                        bulletManager.FireStraight(leftFirePosition.position, 12f + difficultyLevel);
                        bulletManager.FireSpread(leftFirePosition.position, 10 + difficultyLevel, 360f);
                        bulletManager.FireHoming(leftFirePosition.position);
                        bulletManager.FireReflecting(leftFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireRotating(leftFirePosition.position, 240f);
                    }
                    else if (!rightArm.isDestroyed)
                    {
                        bulletManager.FireStraight(rightFirePosition.position, 12f + difficultyLevel);
                        bulletManager.FireSpread(rightFirePosition.position, 10 + difficultyLevel, 360f);
                        bulletManager.FireHoming(rightFirePosition.position);
                        bulletManager.FireReflecting(rightFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireRotating(rightFirePosition.position, 240f);
                    }
                }
                break;

            case BossPattern.Final:
                // ������ ����: �ھ ���� ����
                if (core.vulnerable)
                {
                    bulletManager.FireSplitting(coreFirePosition.position, 6 + difficultyLevel);
                    bulletManager.FireSpread(coreFirePosition.position, 12 + difficultyLevel, 360f);
                }
                break;

            default:
                break;
        }

        if (leftArm.isDestroyed && rightArm.isDestroyed && core.vulnerable)
        {
            // �ھ�� Splitting ź�� ���� (���̵� �ݿ�)
            bulletManager.FireSplitting(coreFirePosition.position, 6 + difficultyLevel);
            // �ھ�� Spread ź�� ���� (360�� Ȯ��)
            bulletManager.FireSpread(coreFirePosition.position, 12 + difficultyLevel, 360f);

            // �ʿ��ϸ� �ٸ� ź�� �Լ��鵵 ȣ���� �� ����
        }
        else if(core.isDestroyed)
        {
            // �ھ �ı��Ǹ� ���� ��ü�� �ı���
            Die();
        }

    }

}