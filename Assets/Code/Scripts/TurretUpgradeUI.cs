using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class TurretUpgradeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button UpgradeAPS;
    [SerializeField] private Button UpgradeRange;
    [SerializeField] private TextMeshProUGUI levelAPSUI;
    [SerializeField] private TextMeshProUGUI costAPSUI;
    [SerializeField] private TextMeshProUGUI levelRangeUI;
    [SerializeField] private TextMeshProUGUI costRangeUI;

    private Turret target;

    public void SetTarget(Turret _target)
    {
        UpgradeAPS.onClick.RemoveListener(upgradeAPS);
        UpgradeRange.onClick.RemoveListener(upgradeRange);

        target = _target;

        updateUI();

        UpgradeAPS.onClick.AddListener(upgradeAPS);
        UpgradeRange.onClick.AddListener(upgradeRange);

        upgradeUI.SetActive(true);
    }

    private void upgradeAPS()
    {
        Debug.Log("Upgrade Speed UI");
        target.UpgradeAPS();
        updateUI();
    }

    private void upgradeRange()
    {
        Debug.Log("Upgrade Range UI");
        target.UpgradeRange();
        updateUI();
    }

    public void Hide()
    {
        upgradeUI.SetActive(false);
    }

    private void updateUI()
    {
        levelAPSUI.text = target.levelAPS.ToString();
        costAPSUI.text = target.CalculateCost(target.levelAPS).ToString();
        levelRangeUI.text = target.levelRange.ToString();
        costRangeUI.text = target.CalculateCost(target.levelRange).ToString();
    }
}
