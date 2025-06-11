using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // �� ź�� ������ ���� ? Inspector���� �Ҵ�
    public GameObject straightBulletPrefab;
    public GameObject spreadBulletPrefab;
    public GameObject homingBulletPrefab;
    public GameObject reflectingBulletPrefab;
    public GameObject rotatingBulletPrefab;
    public GameObject waveBulletPrefab;
    public GameObject delayedBulletPrefab;
    public GameObject gravityBulletPrefab;
    public GameObject splittingBulletPrefab;

    // ���� ź�� ? �׻� �Ʒ���(Vector2.down)���� �߻�
    public void FireStraight(Vector3 position, float speed)
    {
        if (straightBulletPrefab == null)
        {
            Debug.LogWarning("StraightBulletPrefab ���Ҵ�");
            return;
        }
        GameObject bullet = Instantiate(straightBulletPrefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * speed;
        }
    }

    // Ȯ�� ź�� ? �⺻ ������ �Ʒ����� ��������, ��ü �Ѱ�(totalAngle) ������ �л�
    public void FireSpread(Vector3 position, int bulletCount, float totalAngle)
    {
        if (spreadBulletPrefab == null)
        {
            Debug.LogWarning("SpreadBulletPrefab ���Ҵ�");
            return;
        }
        // ��ü ������ �������� �л��Ű��, �⺻ ����(Vector2.down)�� �߽����� ȸ����ŵ�ϴ�.
        float angleStep = (bulletCount > 1) ? totalAngle / (bulletCount - 1) : 0;
        float startAngle = -totalAngle / 2;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle); // Z�� ȸ��
            GameObject bullet = Instantiate(spreadBulletPrefab, position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Vector2.down�� ȸ������ �����Ͽ� �߻��մϴ�.
                rb.velocity = rotation * Vector2.down * 10f; // �⺻ �ӵ� 10f (���� ����)
            }
        }
    }

    // ȣ�� ź�� ? �ʱ� �߻� ������ �Ʒ���, ���� ȣ�� ������ ���� ��ũ��Ʈ�� ����
    public void FireHoming(Vector3 position)
    {
        if (homingBulletPrefab == null)
        {
            Debug.LogWarning("HomingBulletPrefab ���Ҵ�");
            return;
        }
        GameObject bullet = Instantiate(homingBulletPrefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 5f;
        }
    }

    // �ݻ� ź��
    public void FireReflecting(Vector3 position, float speed)
    {
        if (reflectingBulletPrefab == null)
        {
            Debug.LogWarning("ReflectingBulletPrefab ���Ҵ�");
            return;
        }
        GameObject bullet = Instantiate(reflectingBulletPrefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * speed;
        }
    }

    // ȸ�� ź�� ? ź���� ������ RotatingBullet ��ũ��Ʈ�� ȸ�� ȿ���� �����ϰ�, �Ʒ������� �߻�
    public void FireRotating(Vector3 position, float rotationalSpeed)
    {
        if (rotatingBulletPrefab == null)
        {
            Debug.LogWarning("RotatingBulletPrefab ���Ҵ�");
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

    // �ĵ� ź�� ? WaveBullet ��ũ��Ʈ�� ���� �Ʒ��� ���� + ���� ����
    public void FireWave(Vector3 position, float baseSpeed, float frequency, float amplitude)
    {
        if (waveBulletPrefab == null)
        {
            Debug.LogWarning("WaveBulletPrefab ���Ҵ�");
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

    // �ð����� ź�� ? DelayedBullet ��ũ��Ʈ�� ���� ó��, �ʱ� �߻� ������ �Ʒ���
    public void FireDelayed(Vector3 position, float delayTime, float speed)
    {
        if (delayedBulletPrefab == null)
        {
            Debug.LogWarning("DelayedBulletPrefab ���Ҵ�");
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

    // �߷� ź�� ? GravityBullet ��ũ��Ʈ�� Ȱ���Ͽ� �߷¿� ������ �� ����, �ʱ� ������ �Ʒ���
    public void FireGravity(Vector3 position, Transform gravitySource, float gravityBulletForce)
    {
        if (gravityBulletPrefab == null)
        {
            Debug.LogWarning("GravityBulletPrefab ���Ҵ�");
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
            rb.velocity = Vector2.down * 5f; // �ʱ� �ӵ�, �ʿ�� ����
        }
    }

    // �п� ź�� ? SplittingBullet ��ũ��Ʈ�� Ȱ��, �ʱ� �߻� ������ �Ʒ���
    public void FireSplitting(Vector3 position, int bulletCount)
    {
        if (splittingBulletPrefab == null)
        {
            Debug.LogWarning("SplittingBulletPrefab ���Ҵ�");
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

    // ������ ���� ���� ���� ��ȯ ��, �� źȯ ��� �����ϴ� ��ƿ��Ƽ �Լ�
    public void ClearEnemyBullets()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }
        Debug.Log("��� �� ź�� ���ŵ�");
    }
}