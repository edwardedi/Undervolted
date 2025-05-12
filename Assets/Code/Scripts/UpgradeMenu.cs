using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI levelAPSUI;
    [SerializeField] TextMeshProUGUI costAPSUI;
    [SerializeField] TextMeshProUGUI levelRangeUI;
    [SerializeField] TextMeshProUGUI costRangeUI;
    [SerializeField] GameObject specialSkill;
    [SerializeField] Turret turret;

    public static UpgradeMenu main;

    private void Awake()
    {
        main = this;
    }

    private void OnGUI()
    {
        levelAPSUI.text = turret.levelFirstPath.ToString();
        costAPSUI.text = turret.CalculateCost(turret.levelFirstPath).ToString();
        levelRangeUI.text = turret.levelSecondPath.ToString();
        costRangeUI.text = turret.CalculateCost(turret.levelSecondPath).ToString();
    }

    public void SpecialUpgradeAppear()
    {
        specialSkill.SetActive(true);
    }

    public void SpecialUpgradeDisappear()
    {
        specialSkill.SetActive(false);
    }
}
