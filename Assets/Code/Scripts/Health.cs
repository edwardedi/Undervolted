using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyValue = 10;

    private bool isDestroyed = false;
    private Coroutine fireDamageCoroutine;

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyValue);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
    public void ApplyFireDamage(int damagePerTick, float duration, float tickInterval)
    {
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
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
    }
}
