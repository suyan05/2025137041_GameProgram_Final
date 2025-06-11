using UnityEngine;

public abstract class BossPart : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    public bool isDestroyed = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDestroyed) return;

        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    public virtual void Die()
    {
        isDestroyed = true;
        gameObject.SetActive(false);
        Debug.Log($"{gameObject.name} ÆÄ±«µÊ!");
    }

    public virtual void HealFull()
    {
        if (!isDestroyed)
        {
            currentHealth = maxHealth;
            Debug.Log($"{gameObject.name} Ã¼·Â È¸º¹!");
        }
    }
}