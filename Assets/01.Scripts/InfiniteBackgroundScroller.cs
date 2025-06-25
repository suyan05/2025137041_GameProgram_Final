using UnityEngine;

public class InfiniteBackgroundScroller : MonoBehaviour
{
    public Transform[] backgrounds;
    public float scrollSpeed = 2f;
    public float backgroundHeight = 10f;

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            if (bg.position.y <= -backgroundHeight)
            {
                // 아래로 내려간 배경을 맨 위로 이동
                float highestY = GetHighestY();
                bg.position = new Vector3(bg.position.x, highestY + backgroundHeight, bg.position.z);
            }
        }
    }

    float GetHighestY()
    {
        float maxY = float.MinValue;
        foreach (Transform bg in backgrounds)
        {
            if (bg.position.y > maxY)
                maxY = bg.position.y;
        }
        return maxY;
    }
}