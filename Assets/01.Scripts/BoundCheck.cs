// ȭ�� �Ǵ� ���� �Ѱ踦 �Ѿ ��ü�� �����ϴ� ���
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    public float boundary = 30f;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > boundary || Mathf.Abs(transform.position.y) > boundary)
            Destroy(gameObject);
    }
}