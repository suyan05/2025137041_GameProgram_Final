using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("무적 시간 설정")]
    public float invincibilityDuration = 2f;     // 무적 지속 시간 (초)
    public float flickerInterval = 0.1f;         // 깜빡임 간격 (초)
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 무적 상태 타이머는 코루틴 내부에서 제어하므로 별도 업데이트 코드는 없어도 됨.
    }

    public void TakeDamage(float amount)
    {
        if (isInvincible)
            return;

        currentHealth -= amount;
        Debug.Log($"플레이어 체력: {currentHealth}");

        if (currentHealth <= 0f)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFlicker());
        }
    }

    IEnumerator InvincibilityFlicker()
    {
        isInvincible = true;
        float timer = invincibilityDuration;
        while (timer > 0f)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flickerInterval);
            timer -= flickerInterval;
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("플레이어 사망!");
        // 사망 효과, 씬 전환 등 추가 가능
        Destroy(gameObject);
    }
}