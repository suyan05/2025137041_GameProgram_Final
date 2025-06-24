using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public DroneUnit dronePrefab;   // 드론 프리팹
    public Transform player;        // 플레이어 Transform
    public int droneCount = 1;      // 초기 드론 개수

    // 드론 기본 파라미터들
    public float orbitRadius = 2f;
    public float orbitSpeed = 90f;
    public float followSpeed = 5f;
    public float attackInterval = 2f;
    public float bulletSpeed = 10f;

    //public BulletManager bulletManager;  // 필요 시 드론이 BulletManager를 사용할 수 있도록

    // 현재 생성된 드론들을 보관하는 리스트
    private List<DroneUnit> activeDrones = new List<DroneUnit>();

    void Start()
    {
        SpawnDrones(droneCount);
    }

    private void Update()
    {
        // 드론 개수를 키우거나 줄이는 입력 처리
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreaseDroneCount();
        }        
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseDroneCount();
        }
    }

    // droneCount만큼 드론을 생성하고, 플레이어 주위에 원형으로 균등하게 배치합니다.
    void SpawnDrones(int count)
    {
        // 기존에 생성된 드론 모두 제거
        foreach (var drone in activeDrones)
        {
            if (drone != null)
                Destroy(drone.gameObject);
        }
        activeDrones.Clear();

        // 전체 360도를 count로 나누어 각 드론의 오프셋 각을 결정합니다.
        for (int i = 0; i < count; i++)
        {
            DroneUnit newDrone = Instantiate(dronePrefab, player.position, Quaternion.identity);
            newDrone.player = player;
            // 필요 시 BulletManager 참조도 전달 (드론 프리팹에 미리 설정되어 있거나 여기서 설정)
            newDrone.orbitRadius = orbitRadius;
            newDrone.orbitSpeed = orbitSpeed;
            newDrone.followSpeed = followSpeed;
            newDrone.attackInterval = attackInterval;
            newDrone.bulletSpeed = bulletSpeed;

            // 오프셋 각을 0 ~ 360도 범위로 균등하게 분배
            newDrone.offsetAngle = i * (360f / count);

            activeDrones.Add(newDrone);
        }
    }

    // 외부 호출로 드론 개수를 업그레이드할 때 사용합니다.
    public void UpgradeDroneCount(int newCount)
    {
        droneCount = newCount;
        SpawnDrones(droneCount);
    }

    // 드론 개수를 1개 증가시키는 함수 (+ 버튼에 연결)
    public void IncreaseDroneCount()
    {
        droneCount++;
        SpawnDrones(droneCount);
    }

    // 드론 개수를 1개 감소시키는 함수 (_ 또는 - 버튼에 연결)
    public void DecreaseDroneCount()
    {
        // 드론 개수가 0 미만이 되지 않도록 처리
        if (droneCount > 0)
        {
            droneCount--;
            SpawnDrones(droneCount);
        }
    }
}