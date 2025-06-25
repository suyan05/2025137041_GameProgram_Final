using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float CurrentHealth;

    [Header("���� �ð� ����")]
    public float invincibilityDuration = 2f;
    public float flickerInterval = 0.1f;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    [Header("���� ���� ����")]
    public int scorePerSecond = 10;
    private float survivalTimer = 0f;

    private bool isAlive = true;

    void Start()
    {
        CurrentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isAlive || !GameManager.Instance) return;

        survivalTimer += Time.deltaTime;
        if (survivalTimer >= 1f)
        {
            int seconds = Mathf.FloorToInt(survivalTimer);
            GameManager.Instance.AddScore(seconds * scorePerSecond);
            survivalTimer -= seconds;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isInvincible || !isAlive) return;

        CurrentHealth -= amount;
        Debug.Log($"�÷��̾� ü��: {CurrentHealth}");

        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0;
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


    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        CurrentHealth = maxHealth;
    }

    void Die()
    {
        Debug.Log("�÷��̾� ���!");
        isAlive = false;

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.GameOver();

        Destroy(gameObject);
    }
}