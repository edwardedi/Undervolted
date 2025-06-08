using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;

public class TurretUpgradeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button UpgradeAPS;
    [SerializeField] private Button UpgradeRange;
    [SerializeField] private Button SpecialUpgrade;
    [SerializeField] private TextMeshProUGUI levelAPSUI;
    [SerializeField] private TextMeshProUGUI costAPSUI;
    [SerializeField] private TextMeshProUGUI levelRangeUI;
    [SerializeField] private TextMeshProUGUI costRangeUI;
    [SerializeField] private TextMeshProUGUI speedOrSlow;
    [SerializeField] private TextMeshProUGUI rangeOrDamage;

    private Turret target;

    public void SetTarget(Turret _target)
    {
        UpgradeAPS.onClick.RemoveListener(upgradeAPS);
        UpgradeRange.onClick.RemoveListener(upgradeRange);
        SpecialUpgrade.onClick.RemoveListener(specialUpgrade);

        target = _target;

        if (target.typeOfTurret == 2)
            rangeOrDamage.text = "Upgrade Damage";
        else
            rangeOrDamage.text = "Upgrade Range";

        if (target.typeOfTurret == 3)
            speedOrSlow.text = "Upgrade Slowness";
        else
            speedOrSlow.text = "Upgrade Speed";

        updateUI();

        UpgradeAPS.onClick.AddListener(upgradeAPS);
        UpgradeRange.onClick.AddListener(upgradeRange);
        SpecialUpgrade.onClick.AddListener(specialUpgrade);

        upgradeUI.SetActive(true);
    }

    private void upgradeAPS()
    {
        target.UpgradeFirstPath();
        updateUI();
    }

    private void upgradeRange()
    {
        target.UpgradeSecondPath();
        updateUI();
    }

    private void specialUpgrade()
    {
        Debug.Log("SpecialUpgrade UI");
        target.SpecialUpgrade(target);
        updateUI();
    }

    public void Hide()
    {
        upgradeUI.SetActive(false);
    }

    private void updateUI()
    {
        levelAPSUI.text = target.levelFirstPath.ToString();
        costAPSUI.text = target.CalculateCost(target.levelFirstPath).ToString();
        levelRangeUI.text = target.levelSecondPath.ToString();
        costRangeUI.text = target.CalculateCost(target.levelSecondPath).ToString();
    }
}
