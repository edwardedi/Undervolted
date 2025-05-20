using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int hitPoints;
    [SerializeField] private int currencyValue;

    private bool isDestroyed = false;
    private Coroutine fireDamageCoroutine;
    private Coroutine regenerateHealth;
    public bool isRegenerating = false;

    private void Start()
    {
        if (isRegenerating)
        {
            RegenerateHealth(1, 100f, 2f);
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            if (EnemySpawner.main.GetCurrentWave() < 10)
                LevelManager.main.IncreaseCurrency(currencyValue);
            else
                LevelManager.main.IncreaseCurrency(currencyValue / 2);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void Regenerate(int amount)
    {
        if (hitPoints + amount <= maxHealth)
        {
            hitPoints += amount;
        }
    }

    public void ApplyFireDamage(int damagePerTick, float duration, float tickInterval)
    {
        Debug.Log(duration);
        if (fireDamageCoroutine != null)
        {
            StopCoroutine(fireDamageCoroutine);
        }
        fireDamageCoroutine = StartCoroutine(FireDamageOverTime(damagePerTick, duration, tickInterval));
    }

    private IEnumerator FireDamageOverTime(int damagePerTick, float duration, float tickInterval)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage(damagePerTick);
            Debug.Log(elapsed);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
    }

    public void RegenerateHealth(int health, float duration, float tickInterval)
    {
        if (regenerateHealth != null)
        {
            StopCoroutine(regenerateHealth);
        }
        regenerateHealth = StartCoroutine(RegenerateHealthOverTime(health, duration, tickInterval));
    }

    private IEnumerator RegenerateHealthOverTime(int health, float duration, float tickInterval)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Regenerate(health);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
    }
}
