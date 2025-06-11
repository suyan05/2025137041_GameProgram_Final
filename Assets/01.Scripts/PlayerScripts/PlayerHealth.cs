using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("���� �ð� ����")]
    public float invincibilityDuration = 2f;     // ���� ���� �ð� (��)
    public float flickerInterval = 0.1f;         // ������ ���� (��)
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ���� ���� Ÿ�̸Ӵ� �ڷ�ƾ ���ο��� �����ϹǷ� ���� ������Ʈ �ڵ�� ��� ��.
    }

    public void TakeDamage(float amount)
    {
        if (isInvincible)
            return;

        currentHealth -= amount;
        Debug.Log($"�÷��̾� ü��: {currentHealth}");

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
        Debug.Log("�÷��̾� ���!");
        // ��� ȿ��, �� ��ȯ �� �߰� ����
        Destroy(gameObject);
    }
}