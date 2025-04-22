using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRoationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;


    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float aps = 2f;
    [SerializeField] private int baseUpgradeCost = 100;
    [SerializeField] private int electricityCost = 5;
    [SerializeField] private float freezeTime = 1f;

    private Transform target;
    private float timeUntilFire;
    private int level = 1;
    private float apsBase;
    private float targetingRangeBase;
    private bool isGridConnected;
    private int typeOfTurret;

    private void Start()
    {
        apsBase = aps;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update()
    {
        if (isGridConnected == false) return;
        if (LevelManager.main.CheckElectricity(electricityCost) == false) return;

        if (typeOfTurret == 2)
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                FreezeEnemies();
                timeUntilFire = 0f;
            }
            return;
        }

        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                if (LevelManager.main.ConsumeElectricity(electricityCost) == true)
                {
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
        }
    }

    public void setTypeOfTurret(int type)
    {
        switch (type)
        {
            case 0:
                typeOfTurret = 0; break;
            case 1:
                typeOfTurret = 1; break;
            case 2:
                typeOfTurret = 2; break;
            case 3:
                typeOfTurret = 3; break;
            case 4:
                typeOfTurret = 4; break;
            case 5:
                typeOfTurret = 5; break;
            case 6:
                typeOfTurret = 6; break;
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        //float maxDistance = float.MaxValue;
        if (enemies.Length > 0)
        {
            /*foreach (RaycastHit2D enemy in enemies)
            {
                EnemyMovement em = enemy.transform.GetComponent<EnemyMovement>();
                if (em.DistanceToFinish() > maxDistance)
                {
                    maxDistance = em.DistanceToFinish();
                }
            }
            foreach (var enemy in enemies)
            {
                EnemyMovement em = enemy.transform.GetComponent<EnemyMovement>();
                if (em.DistanceToFinish() - maxDistance < 0.00001)
                {
                    target = enemy.transform;
                }
            }*/
            target = enemies[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRoationPoint.rotation = Quaternion.RotateTowards(turretRoationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime); //= targetRotation; for instant rotation
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    public void openUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        //UIManager.main.SetHoveringState(false);
    }

    private void Upgrade()
    {
        if (CalculateCost() > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());
        level++;
        aps = Calculateaps();
        targetingRange = CalculateRange();

        //CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float Calculateaps()
    {
        return apsBase * Mathf.Pow(level, 0.5f);
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.2f);
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            if (LevelManager.main.ConsumeElectricity(electricityCost) == true)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                    em.UpdateSpeed(em.GetBaseSpeed() / (level + 1));

                    StartCoroutine(ResetEnemySpeed(em));
                }
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
