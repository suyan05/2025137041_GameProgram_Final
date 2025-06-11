using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // 각 탄막 프리팹 참조 ? Inspector에서 할당
    public GameObject straightBulletPrefab;
    public GameObject spreadBulletPrefab;
    public GameObject homingBulletPrefab;
    public GameObject reflectingBulletPrefab;
    public GameObject rotatingBulletPrefab;
    public GameObject waveBulletPrefab;
    public GameObject delayedBulletPrefab;
    public GameObject gravityBulletPrefab;
    public GameObject splittingBulletPrefab;

    // 직선 탄막 ? 항상 아래쪽(Vector2.down)으로 발사
    public void FireStraight(Vector3 position, float speed)
    {
        if (straightBulletPrefab == null)
        {
            Debug.LogWarning("StraightBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(straightBulletPrefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * speed;
        }
    }

    // 확산 탄막 ? 기본 방향은 아래쪽을 기준으로, 전체 총각(totalAngle) 내에서 분산
    public void FireSpread(Vector3 position, int bulletCount, float totalAngle)
    {
        if (spreadBulletPrefab == null)
        {
            Debug.LogWarning("SpreadBulletPrefab 미할당");
            return;
        }
        // 전체 각도를 기준으로 분산시키되, 기본 방향(Vector2.down)을 중심으로 회전시킵니다.
        float angleStep = (bulletCount > 1) ? totalAngle / (bulletCount - 1) : 0;
        float startAngle = -totalAngle / 2;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle); // Z축 회전
            GameObject bullet = Instantiate(spreadBulletPrefab, position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Vector2.down에 회전값을 적용하여 발사합니다.
                rb.velocity = rotation * Vector2.down * 10f; // 기본 속도 10f (조정 가능)
            }
        }
    }

    // 호밍 탄막 ? 초기 발사 방향은 아래쪽, 이후 호밍 로직은 개별 스크립트로 구현
    public void FireHoming(Vector3 position)
    {
        if (homingBulletPrefab == null)
        {
            Debug.LogWarning("HomingBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(homingBulletPrefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 5f;
        }
    }

    // 반사 탄막
    public void FireReflecting(Vector3 position, float speed)
    {
        if (reflectingBulletPrefab == null)
        {
            Debug.LogWarning("ReflectingBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(reflectingBulletPrefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * speed;
        }
    }

    // 회전 탄막 ? 탄막에 부착된 RotatingBullet 스크립트로 회전 효과를 적용하고, 아래쪽으로 발사
    public void FireRotating(Vector3 position, float rotationalSpeed)
    {
        if (rotatingBulletPrefab == null)
        {
            Debug.LogWarning("RotatingBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(rotatingBulletPrefab, position, Quaternion.identity);
        RotatingBullet rotatingScript = bullet.GetComponent<RotatingBullet>();
        if (rotatingScript != null)
        {
            rotatingScript.rotationSpeed = rotationalSpeed;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 10f;
        }
    }

    // 파동 탄막 ? WaveBullet 스크립트를 통해 아래쪽 진행 + 수평 진동
    public void FireWave(Vector3 position, float baseSpeed, float frequency, float amplitude)
    {
        if (waveBulletPrefab == null)
        {
            Debug.LogWarning("WaveBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(waveBulletPrefab, position, Quaternion.identity);
        WaveBullet waveScript = bullet.GetComponent<WaveBullet>();
        if (waveScript != null)
        {
            waveScript.baseSpeed = baseSpeed;
            waveScript.frequency = frequency;
            waveScript.amplitude = amplitude;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * baseSpeed;
        }
    }

    // 시간지연 탄막 ? DelayedBullet 스크립트를 통해 처리, 초기 발사 방향은 아래쪽
    public void FireDelayed(Vector3 position, float delayTime, float speed)
    {
        if (delayedBulletPrefab == null)
        {
            Debug.LogWarning("DelayedBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(delayedBulletPrefab, position, Quaternion.identity);
        DelayedBullet delayedScript = bullet.GetComponent<DelayedBullet>();
        if (delayedScript != null)
        {
            delayedScript.delayTime = delayTime;
            delayedScript.speed = speed;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * speed;
        }
    }

    // 중력 탄막 ? GravityBullet 스크립트를 활용하여 중력원 방향의 힘 적용, 초기 방향은 아래쪽
    public void FireGravity(Vector3 position, Transform gravitySource, float gravityBulletForce)
    {
        if (gravityBulletPrefab == null)
        {
            Debug.LogWarning("GravityBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(gravityBulletPrefab, position, Quaternion.identity);
        GravityBullet gravityScript = bullet.GetComponent<GravityBullet>();
        if (gravityScript != null)
        {
            gravityScript.gravitySource = gravitySource;
            gravityScript.gravityForce = gravityBulletForce;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 5f; // 초기 속도, 필요시 조정
        }
    }

    // 분열 탄막 ? SplittingBullet 스크립트를 활용, 초기 발사 방향은 아래쪽
    public void FireSplitting(Vector3 position, int bulletCount)
    {
        if (splittingBulletPrefab == null)
        {
            Debug.LogWarning("SplittingBulletPrefab 미할당");
            return;
        }
        GameObject bullet = Instantiate(splittingBulletPrefab, position, Quaternion.identity);
        SplittingBullet splittingScript = bullet.GetComponent<SplittingBullet>();
        if (splittingScript != null)
        {
            splittingScript.splitCount = bulletCount;
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 10f;
        }
    }

    // 보스가 죽을 때나 게임 전환 시, 적 탄환 모두 제거하는 유틸리티 함수
    public void ClearEnemyBullets()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }
        Debug.Log("모든 적 탄막 제거됨");
    }
}