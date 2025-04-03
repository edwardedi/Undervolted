using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI levelUI;
    [SerializeField] TextMeshProUGUI electricityUI;

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        levelUI.text = EnemySpawner.main.GetCurrentWave().ToString();
        electricityUI.text = LevelManager.main.electricity.ToString();
    }
}
