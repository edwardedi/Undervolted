using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Button nextLevelButton;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public static EnemySpawner main;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;
    private int numberOfSpawnedEnemies;

    public int GetCurrentWave()
    {
        return currentWave;
    }

    private void Start()
    {
        nextLevelButton.onClick.AddListener(StartWave);
    }

    private void Awake()
    {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Update()
    {
        if (!isSpawning)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            numberOfSpawnedEnemies = SpawnEnemy();
            enemiesLeftToSpawn -= numberOfSpawnedEnemies;
            enemiesAlive += numberOfSpawnedEnemies;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void StartWave()
    {
        if (isSpawning)
            return;
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        currentWave++;
        isSpawning = false;
        timeSinceLastSpawn = 0f;
    }

    private int SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        GameObject enemy1 = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        EnemyMovement enemyMovement1 = enemy1.GetComponent<EnemyMovement>();
        if (enemyMovement1 != null)
        {
            enemyMovement1.pathIndex = 0;
        }

        if (LevelManager.main.secondStartPoint != null)
        {
            GameObject enemy2 = Instantiate(prefabToSpawn, LevelManager.main.secondStartPoint.position, Quaternion.identity);
            EnemyMovement enemyMovement2 = enemy2.GetComponent<EnemyMovement>();
            if (enemyMovement2 != null)
            {
                enemyMovement2.pathIndex = 1;
                enemyMovement1.numberOfEntryPoints = 2;
                enemyMovement2.numberOfEntryPoints = 2;
            }
            return 2;
        }
        return 1;
    }

    private int EnemiesPerWave()
    {
        int result = Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        if (result % 2 == 1)
            return result + 1;
        return result;
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor),0f, enemiesPerSecondCap);
    }

    public bool IsLevelActive()
    {
        return isSpawning;
    }
}
