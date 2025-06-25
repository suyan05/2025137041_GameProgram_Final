using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float maxHP = 10;
    public int coinReward = 5;
    public int scoreReward = 100;

    private float currentHP;
    private bool isOutOfBounds = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f; // ±ôºýÀÓ ½Ã°£

    void Start()
    {
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x < -0.1f || viewPos.x > 1.1f || viewPos.y < -0.1f || viewPos.y > 1.1f)
        {
            isOutOfBounds = true;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (spriteRenderer != null)
            StartCoroutine(FlashOnDamage());

        if (currentHP <= 0)
            Die();
    }

    System.Collections.IEnumerator FlashOnDamage()
    {
        spriteRenderer.color = Color.red; //»¡°£»öÀ¸·Î ±ôºý
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        if (!isOutOfBounds && GameManager.Instance != null)
        {
            GameManager.Instance.AddCoin(coinReward);
            GameManager.Instance.AddScore(scoreReward);
        }

        Destroy(gameObject);
    }
}