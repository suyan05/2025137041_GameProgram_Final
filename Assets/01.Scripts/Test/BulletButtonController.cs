using UnityEngine;
using UnityEngine.UI;

public class BulletButtonController : MonoBehaviour
{
    // BulletManager ���� (��� ź�� �Լ��� �����Ǿ� ����)
    public BulletManager bulletManager;
    // ź���� ������ ��ġ (��: �ѱ� ��ġ)
    public Transform firePosition;

    // ���� ź�� �߻�� �ӵ�
    public float straightSpeed = 10f;

    // Ȯ�� ź�� ����
    public int spreadBulletCount = 8;
    public float spreadTotalAngle = 90f;

    // ȣ�� ź���� ������ �Ķ���� ���� �ʱ� �߻� �ӵ��� ���� (ȣ�� ������ ���� ��ũ��Ʈ���� ����)
    public float homingInitialSpeed = 5f;

    // �ݻ� ź�� �߻�� �ӵ�
    public float reflectingSpeed = 6f;

    // ȸ�� ź�� ���� (ȸ�� �ӵ�)
    public float rotatingRotationalSpeed = 180f;

    // �ĵ� ź�� ����
    public float waveBaseSpeed = 5f;
    public float waveFrequency = 2f;
    public float waveAmplitude = 2f;

    // �ð����� ź�� ����
    public float delayedDelayTime = 1f;
    public float delayedSpeed = 7f;

    // �߷� ź�� ����
    public Transform gravitySource;  // �߷� ��(��: ���� �Ǵ� Ư�� ����)
    public float gravityBulletForce = 9.8f;

    // �п� ź�� ����
    public int splittingBulletCount = 6;


    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Space))
        {
            Application.Quit();
            Debug.Log("���� ����");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnFireStraightButton();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnFireSpreadButton();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnFireHomingButton();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnFireReflectingButton();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnFireWaveButton();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            OnFireDelayedButton();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            OnFireGravityButton();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnFireSplittingButton();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnFireRotatingButton();
        }
    }

    // ���� ź�� �߻� �Լ� (��ư OnClick���� ȣ��)
    public void OnFireStraightButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireStraight(firePosition.position, straightSpeed);
            Debug.Log("���� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // Ȯ�� ź�� �߻� �Լ�
    public void OnFireSpreadButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireSpread(firePosition.position, spreadBulletCount, spreadTotalAngle);
            Debug.Log("Ȯ�� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // ȣ�� ź�� �߻� �Լ�
    public void OnFireHomingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireHoming(firePosition.position);
            Debug.Log("ȣ�� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // �ݻ� ź�� �߻� �Լ�
    public void OnFireReflectingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireReflecting(firePosition.position, reflectingSpeed);
            Debug.Log("�ݻ� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // ȸ�� ź�� �߻� �Լ�
    public void OnFireRotatingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireRotating(firePosition.position, rotatingRotationalSpeed);
            Debug.Log("ȸ�� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // �ĵ� ź�� �߻� �Լ�
    public void OnFireWaveButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireWave(firePosition.position, waveBaseSpeed, waveFrequency, waveAmplitude);
            Debug.Log("�ĵ� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // �ð����� ź�� �߻� �Լ�
    public void OnFireDelayedButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireDelayed(firePosition.position, delayedDelayTime, delayedSpeed);
            Debug.Log("�ð����� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

    // �߷� ź�� �߻� �Լ�
    public void OnFireGravityButton()
    {
        if (bulletManager != null && firePosition != null && gravitySource != null)
        {
            bulletManager.FireGravity(firePosition.position, gravitySource, gravityBulletForce);
            Debug.Log("�߷� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager, FirePosition, �Ǵ� GravitySource�� �̼�����");
        }
    }

    // �п� ź�� �߻� �Լ�
    public void OnFireSplittingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireSplitting(firePosition.position, splittingBulletCount);
            Debug.Log("�п� ź�� �߻�");
        }
        else
        {
            Debug.LogWarning("BulletManager �Ǵ� FirePosition�� �������� �ʾҽ��ϴ�.");
        }
    }

}