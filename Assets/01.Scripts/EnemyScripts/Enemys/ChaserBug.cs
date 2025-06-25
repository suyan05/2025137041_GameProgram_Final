using UnityEngine;

public class ChaserBug : MonoBehaviour, IActivatablePattern
{
    public string playerObjectName = "Player"; // ã�� �÷��̾� ������Ʈ �̸�
    public float moveSpeed = 3f;

    public Vector2 offsetFromPlayer = new Vector2(0f, 7f); // �÷��̾�κ��� ������ ��� ��ġ
    public float followSpeed = 3f;               // ���󰡴� �ӵ�

    private Transform playerTransform;


    public MonoBehaviour[] shooters;

    private bool isActive = false;

    void Start()
    {
        // �̸����� �÷��̾� Transform ã��
        GameObject playerObj = GameObject.Find(playerObjectName);
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogWarning($"ChaserBug: '{playerObjectName}' ������Ʈ�� ã�� �� �����ϴ�.");

        StartPattern();
    }


    void Update()
    {
        if (playerTransform == null) return;

        // ��ǥ ��ġ = �÷��̾� ��ġ + ��� ������
        Vector3 targetPos = playerTransform.position + (Vector3)offsetFromPlayer;

        // ���� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵�
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