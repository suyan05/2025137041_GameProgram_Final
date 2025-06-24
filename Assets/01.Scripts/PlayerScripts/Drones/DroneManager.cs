using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public DroneUnit dronePrefab;   // ��� ������
    public Transform player;        // �÷��̾� Transform
    public int droneCount = 1;      // �ʱ� ��� ����

    // ��� �⺻ �Ķ���͵�
    public float orbitRadius = 2f;
    public float orbitSpeed = 90f;
    public float followSpeed = 5f;
    public float attackInterval = 2f;
    public float bulletSpeed = 10f;

    //public BulletManager bulletManager;  // �ʿ� �� ����� BulletManager�� ����� �� �ֵ���

    // ���� ������ ��е��� �����ϴ� ����Ʈ
    private List<DroneUnit> activeDrones = new List<DroneUnit>();

    void Start()
    {
        SpawnDrones(droneCount);
    }

    private void Update()
    {
        // ��� ������ Ű��ų� ���̴� �Է� ó��
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IncreaseDroneCount();
        }        
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseDroneCount();
        }
    }

    // droneCount��ŭ ����� �����ϰ�, �÷��̾� ������ �������� �յ��ϰ� ��ġ�մϴ�.
    void SpawnDrones(int count)
    {
        // ������ ������ ��� ��� ����
        foreach (var drone in activeDrones)
        {
            if (drone != null)
                Destroy(drone.gameObject);
        }
        activeDrones.Clear();

        // ��ü 360���� count�� ������ �� ����� ������ ���� �����մϴ�.
        for (int i = 0; i < count; i++)
        {
            DroneUnit newDrone = Instantiate(dronePrefab, player.position, Quaternion.identity);
            newDrone.player = player;
            // �ʿ� �� BulletManager ������ ���� (��� �����տ� �̸� �����Ǿ� �ְų� ���⼭ ����)
            newDrone.orbitRadius = orbitRadius;
            newDrone.orbitSpeed = orbitSpeed;
            newDrone.followSpeed = followSpeed;
            newDrone.attackInterval = attackInterval;
            newDrone.bulletSpeed = bulletSpeed;

            // ������ ���� 0 ~ 360�� ������ �յ��ϰ� �й�
            newDrone.offsetAngle = i * (360f / count);

            activeDrones.Add(newDrone);
        }
    }

    // �ܺ� ȣ��� ��� ������ ���׷��̵��� �� ����մϴ�.
    public void UpgradeDroneCount(int newCount)
    {
        droneCount = newCount;
        SpawnDrones(droneCount);
    }

    // ��� ������ 1�� ������Ű�� �Լ� (+ ��ư�� ����)
    public void IncreaseDroneCount()
    {
        droneCount++;
        SpawnDrones(droneCount);
    }

    // ��� ������ 1�� ���ҽ�Ű�� �Լ� (_ �Ǵ� - ��ư�� ����)
    public void DecreaseDroneCount()
    {
        // ��� ������ 0 �̸��� ���� �ʵ��� ó��
        if (droneCount > 0)
        {
            droneCount--;
            SpawnDrones(droneCount);
        }
    }
}