using UnityEngine;

public enum BossPattern
{
    Initial,    // 초기 패턴
    Enhanced,   // 강화 패턴
    Defensive,  // 방어 패턴
    Pressure,   // 압박 패턴
    Difficulty, // 난이도 상승 패턴
    Special,    // 특수 패턴 (체력 30% 이하)
    Critical,   // 위기 패턴 (체력 15% 이하)
    Final       // 마무리 패턴 (체력 5% 이하)
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
    public float maxHealth;      // 보스 전체 최대 체력 (각 부위의 합)
    private float currentHealth; // 보스 전체 현재 체력

    // 두 팔 모두 살아 있을 때 번갈아 발사하기 위한 변수
    private bool fireLeftNext = true;
    // 부서짐 감지를 위한 플래그
    private bool leftArmNotified = false;
    private bool rightArmNotified = false;

    void Start()
    {
        // 각 부위의 최대 체력을 합산해서 보스 maxHealth를 설정
        maxHealth = leftArm.maxHealth + rightArm.maxHealth + core.maxHealth;

        // 보스의 초기 체력은 각 부위의 현재 체력 합산
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

        // 왼쪽 팔 부서짐 감지
        if (leftArm.isDestroyed && !leftArmNotified)
        {
            Debug.Log("왼쪽 팔이 부서졌습니다!");
            leftArmNotified = true;
            difficultyLevel++;
            // 추가 효과 및 난이도 상승 처리 가능
        }

        // 오른쪽 팔 부서짐 감지
        if (rightArm.isDestroyed && !rightArmNotified)
        {
            Debug.Log("오른쪽 팔이 부서졌습니다!");
            rightArmNotified = true;
            difficultyLevel++;
        }

        // 양 팔이 모두 부서지면 코어를 공격가능하게 설정
        if (leftArm.isDestroyed && rightArm.isDestroyed && !core.vulnerable)
        {
            core.vulnerable = true;
            Debug.Log("보스 코어가 공격 가능해졌습니다!");
            difficultyLevel++;
        }

        // 체력 기반 패턴 전환 (예시)
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
        Debug.Log("보스 전체 체력: " + currentHealth);
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("보스 격파!");
        // 보스 죽음 이후 탄막 생성이 중단됩니다.
        // 추가로 BulletManager를 통해 장면에 남아있는 모든 적 탄환을 삭제합니다.
        if (bulletManager != null)
        {
            bulletManager.ClearEnemyBullets();
        }
        // 보스 오브젝트 파괴
        Destroy(gameObject);
    }



    // 보스 탄막 패턴 실행 메서드
    void ExecutePattern()
    {
        switch (currentPattern)
        {
            case BossPattern.Initial:
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        // 번갈아가며 발사
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
                // 강화 패턴: 탄막 속도 증가와 추적 탄막 추가
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
                // 방어 패턴: 반사 탄막으로 방어
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
                // 압박 패턴: 빠른 연사 탄막과 회전 탄막
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
                // 난이도 상승 패턴: 탄막 밀도 증가 및 파동 탄막 사용
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
                // 특수 패턴: 시간지연 탄막과 중력 탄막
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
                // 위기 패턴: 모든 탄막 조합
                if (!leftArm.isDestroyed || !rightArm.isDestroyed)
                {
                    if (!leftArm.isDestroyed && !rightArm.isDestroyed)
                    {
                        // 왼쪽 팔의 탄막에 난이도 반영
                        bulletManager.FireStraight(leftFirePosition.position, 12f + difficultyLevel);
                        bulletManager.FireSpread(leftFirePosition.position, 10 + difficultyLevel, 360f);
                        bulletManager.FireHoming(leftFirePosition.position);
                        bulletManager.FireReflecting(leftFirePosition.position, 8f + difficultyLevel);
                        bulletManager.FireRotating(leftFirePosition.position, 240f);

                        // 오른쪽 팔의 탄막에 난이도 반영
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
                // 마무리 패턴: 코어가 최종 공격
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
            // 코어에서 Splitting 탄막 생성 (난이도 반영)
            bulletManager.FireSplitting(coreFirePosition.position, 6 + difficultyLevel);
            // 코어에서 Spread 탄막 생성 (360도 확산)
            bulletManager.FireSpread(coreFirePosition.position, 12 + difficultyLevel, 360f);

            // 필요하면 다른 탄막 함수들도 호출할 수 있음
        }
        else if(core.isDestroyed)
        {
            // 코어가 파괴되면 보스 전체가 파괴됨
            Die();
        }

    }

}