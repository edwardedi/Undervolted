/*using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float aps = 10f; //attack per second
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;
    private float turretLevel = 1;
    private bool isGridConnected;

    private void Update()
    {
        if (isGridConnected == false) return;
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(em.GetBaseSpeed() / (turretLevel + 1));

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    public void SetGridConnectivity(bool isConnected)
    {
        isGridConnected = isConnected;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
*/