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
        levelAPSUI.text = turret.levelAPS.ToString();
        costAPSUI.text = turret.CalculateCost(turret.levelAPS).ToString();
        levelRangeUI.text = turret.levelRange.ToString();
        costRangeUI.text = turret.CalculateCost(turret.levelRange).ToString();
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
