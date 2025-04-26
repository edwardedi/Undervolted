using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI levelAPSUI;
    [SerializeField] TextMeshProUGUI costAPSUI;
    [SerializeField] TextMeshProUGUI levelRangeUI;
    [SerializeField] TextMeshProUGUI costRangeUI;
    [SerializeField] Turret turret;

    private void OnGUI()
    {
        levelAPSUI.text = turret.levelAPS.ToString();
        costAPSUI.text = turret.CalculateCost(turret.levelAPS).ToString();
        levelRangeUI.text = turret.levelRange.ToString();
        costRangeUI.text = turret.CalculateCost(turret.levelRange).ToString();
    }
}
