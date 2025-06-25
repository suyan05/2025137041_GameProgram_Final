using UnityEngine;

public class ChaserBug : MonoBehaviour, IActivatablePattern
{
    public string playerObjectName = "Player"; // 찾을 플레이어 오브젝트 이름
    public float moveSpeed = 3f;

    public Vector2 offsetFromPlayer = new Vector2(0f, 7f); // 플레이어로부터 유지할 상대 위치
    public float followSpeed = 3f;               // 따라가는 속도

    private Transform playerTransform;


    public MonoBehaviour[] shooters;

    private bool isActive = false;

    void Start()
    {
        // 이름으로 플레이어 Transform 찾기
        GameObject playerObj = GameObject.Find(playerObjectName);
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogWarning($"ChaserBug: '{playerObjectName}' 오브젝트를 찾을 수 없습니다.");

        StartPattern();
    }


    void Update()
    {
        if (playerTransform == null) return;

        // 목표 위치 = 플레이어 위치 + 상대 오프셋
        Vector3 targetPos = playerTransform.position + (Vector3)offsetFromPlayer;

        // 현재 위치에서 목표 위치로 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);


    }

    public void StartPattern()
    {
        isActive = true;
        foreach (var s in shooters)
            if (s is IActivatablePattern p) p.StartPattern();
    }

    public void StopPattern()
    {
        isActive = false;
        foreach (var s in shooters)
            if (s is IActivatablePattern p) p.StopPattern();
    }
}