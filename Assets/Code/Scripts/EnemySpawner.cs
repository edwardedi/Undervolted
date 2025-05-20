using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Button nextLevelButton;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies;
    [SerializeField] private float enemiesPerSecond;
    [SerializeField] private float difficultyScalingFactor;

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
    private int typesOfEnemiesToSpawn;
    private int enemiesPerType;
    private int spawnedNumber = 0;
    private int typeOfTheEnemy = 0;
    private int numberOfEntryPoints = 1;

    public int GetCurrentWave()
    {
        return currentWave;
    }

    private void Start()
    {
        if (LevelManager.main.secondStartPoint != null)
            numberOfEntryPoints = 2;
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

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            if (numberOfEntryPoints == 2)
                spawnedNumber += 2;
            else
                spawnedNumber += 1;
            if (spawnedNumber > enemiesPerType)
            {
                typeOfTheEnemy += 1;
                spawnedNumber = 0;
            }
            numberOfSpawnedEnemies = SpawnEnemy(typeOfTheEnemy);
            enemiesLeftToSpawn -= numberOfSpawnedEnemies;
            enemiesAlive += numberOfSpawnedEnemies;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            spawnedNumber = 0;
            typeOfTheEnemy = 0;
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
        typesOfEnemiesToSpawn = GetCurrentWave() / 5 + 1;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
        enemiesPerType = EnemiesPerWave() / typesOfEnemiesToSpawn + 1;
    }

    private void EndWave()
    {
        currentWave++;
        isSpawning = false;
        timeSinceLastSpawn = 0f;
    }

    private int SpawnEnemy(int enemyIndex)
    {
        int index = enemyIndex; //Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        GameObject enemy1 = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        EnemyMovement enemyMovement1 = enemy1.GetComponent<EnemyMovement>();
        if (enemyMovement1 != null)
        {
            enemyMovement1.pathIndex = 0;
        }

        if (numberOfEntryPoints == 2)
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
        int result = Mathf.RoundToInt(baseEnemies + (currentWave - 1) * difficultyScalingFactor * 20);
        if (result % 2 == 1)
            return result + 1;
        return result;
    }

    private float EnemiesPerSecond()
    {
        return enemiesPerSecond + currentWave * difficultyScalingFactor;
    }

    public bool IsLevelActive()
    {
        return isSpawning;
    }
}
