using UnityEngine;

public class SplittingBullet : Bullet
{
    public GameObject splitBulletPrefab;     // 분열되어 나올 탄막 프리팹
    public int splitCount = 5;               // 몇 개로 분열할지
    public float splitAngleRange = 180f;     // 몇 도 범위로 퍼질지
    public float splitSpeed = 5f;            // 분열 탄막의 속도

    protected override void Start()
    {
        base.Start();
        Invoke(nameof(Split), lifetime);     // 생존 시간 후 분열
    }

    private void Split()
    {
        float angleStep = splitAngleRange / (splitCount - 1);
        float startAngle = -splitAngleRange / 2f;

        for (int i = 0; i < splitCount; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = Instantiate(splitBulletPrefab, transform.position, Quaternion.identity);
            Bullet b = bullet.GetComponent<Bullet>();
            if (b != null)
            {
                b.Setup(dir, splitSpeed);
                b.bulletType = bulletType;
            }
        }

        Destroy(gameObject); // 원래 탄막은 제거
    }
}