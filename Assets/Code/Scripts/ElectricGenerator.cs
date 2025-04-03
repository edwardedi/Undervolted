using System.Security.Cryptography;
using UnityEngine;

public class ElectricGenerator : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private float gps = 2f; // Generate per second

    private float timeUntilFire;

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / gps)
        {
            if (EnemySpawner.main.IsLevelActive() == true)
                LevelManager.main.GenerateElectricity(5);
            timeUntilFire = 0f;
        }
    }
}
