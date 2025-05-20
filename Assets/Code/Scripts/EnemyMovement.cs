using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed;

    private Transform target;
    public int pathIndex = 0;
    private float baseSpeed;
    public int numberOfEntryPoints = 1;
    private float distanceToEnd = 0;

    void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    void Update()
    {
        if (numberOfEntryPoints == 1)
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.05f)
            {
                pathIndex++;

                if (pathIndex >= LevelManager.main.path.Length)
                {
                    EnemySpawner.onEnemyDestroy.Invoke();
                    Destroy(gameObject);
                    LevelManager.main.decreaseLife();
                    return;
                }
                else
                {
                    target = LevelManager.main.path[pathIndex];
                }
            }
        } else if (numberOfEntryPoints == 2)
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.05f)
            {
                pathIndex += 2;

                if (pathIndex >= LevelManager.main.path.Length)
                {
                    EnemySpawner.onEnemyDestroy.Invoke();
                    Destroy(gameObject);
                    LevelManager.main.decreaseLife();
                    return;
                }
                else
                {
                    target = LevelManager.main.path[pathIndex];
                }
            }
        }
        //distanceToEnd = DistanceToFinish();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    public float GetBaseSpeed()
    {
        return baseSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    public float DistanceToFinish()
    {
        float totalDistance = Vector2.Distance(LevelManager.main.path[pathIndex].position, transform.position);
        for (int i = pathIndex + 1; i < LevelManager.main.path.Length; i++)
        {
            totalDistance += Vector2.Distance(LevelManager.main.path[i - 1].position, LevelManager.main.path[i].position);
        }
        return totalDistance;
    }
}
