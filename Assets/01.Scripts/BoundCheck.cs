// 화면 또는 월드 한계를 넘어간 객체를 제거하는 기능
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