using UnityEngine;

public class SpiralBullet : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    public void Setup(float s, float r)
    {
        speed = s;
        rotationSpeed = r;
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}