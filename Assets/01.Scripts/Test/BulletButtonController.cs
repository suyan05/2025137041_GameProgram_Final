using UnityEngine;
using UnityEngine.UI;

public class BulletButtonController : MonoBehaviour
{
    // BulletManager 참조 (모든 탄막 함수가 구현되어 있음)
    public BulletManager bulletManager;
    // 탄막이 생성될 위치 (예: 총구 위치)
    public Transform firePosition;

    // 직선 탄막 발사용 속도
    public float straightSpeed = 10f;

    // 확산 탄막 설정
    public int spreadBulletCount = 8;
    public float spreadTotalAngle = 90f;

    // 호밍 탄막는 별도의 파라미터 없이 초기 발사 속도만 지정 (호밍 로직은 개별 스크립트에서 구현)
    public float homingInitialSpeed = 5f;

    // 반사 탄막 발사용 속도
    public float reflectingSpeed = 6f;

    // 회전 탄막 설정 (회전 속도)
    public float rotatingRotationalSpeed = 180f;

    // 파동 탄막 설정
    public float waveBaseSpeed = 5f;
    public float waveFrequency = 2f;
    public float waveAmplitude = 2f;

    // 시간지연 탄막 설정
    public float delayedDelayTime = 1f;
    public float delayedSpeed = 7f;

    // 중력 탄막 설정
    public Transform gravitySource;  // 중력 원(예: 보스 또는 특정 지점)
    public float gravityBulletForce = 9.8f;

    // 분열 탄막 설정
    public int splittingBulletCount = 6;


    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Space))
        {
            Application.Quit();
            Debug.Log("게임 종료");
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

    // 직선 탄막 발사 함수 (버튼 OnClick에서 호출)
    public void OnFireStraightButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireStraight(firePosition.position, straightSpeed);
            Debug.Log("직선 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 확산 탄막 발사 함수
    public void OnFireSpreadButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireSpread(firePosition.position, spreadBulletCount, spreadTotalAngle);
            Debug.Log("확산 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 호밍 탄막 발사 함수
    public void OnFireHomingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireHoming(firePosition.position);
            Debug.Log("호밍 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 반사 탄막 발사 함수
    public void OnFireReflectingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireReflecting(firePosition.position, reflectingSpeed);
            Debug.Log("반사 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 회전 탄막 발사 함수
    public void OnFireRotatingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireRotating(firePosition.position, rotatingRotationalSpeed);
            Debug.Log("회전 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 파동 탄막 발사 함수
    public void OnFireWaveButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireWave(firePosition.position, waveBaseSpeed, waveFrequency, waveAmplitude);
            Debug.Log("파동 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 시간지연 탄막 발사 함수
    public void OnFireDelayedButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireDelayed(firePosition.position, delayedDelayTime, delayedSpeed);
            Debug.Log("시간지연 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

    // 중력 탄막 발사 함수
    public void OnFireGravityButton()
    {
        if (bulletManager != null && firePosition != null && gravitySource != null)
        {
            bulletManager.FireGravity(firePosition.position, gravitySource, gravityBulletForce);
            Debug.Log("중력 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager, FirePosition, 또는 GravitySource가 미설정됨");
        }
    }

    // 분열 탄막 발사 함수
    public void OnFireSplittingButton()
    {
        if (bulletManager != null && firePosition != null)
        {
            bulletManager.FireSplitting(firePosition.position, splittingBulletCount);
            Debug.Log("분열 탄막 발사");
        }
        else
        {
            Debug.LogWarning("BulletManager 또는 FirePosition이 설정되지 않았습니다.");
        }
    }

}