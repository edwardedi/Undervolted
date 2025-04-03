using System;
using System.Reflection;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    private GameObject gridObj;
    public Turret turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUi()) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.name == "ElectricGrid" && gridObj == null)
        {
            LevelManager.main.SpendCurrency(towerToBuild.cost);
            gridObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            turret = towerObj.GetComponent<Turret>();
            turret.SetGridConnectivity(true);
            BuildManager.main.SetSelectedTower(-1);
            return;
        }

        if (towerObj != null)
        {
            turret.openUpgradeUI();
            return;
        }

        if (towerToBuild == null) return;

        if (towerToBuild.cost > LevelManager.main.currency)
        {
            //message you can't afford
            return;
        }

        if (towerToBuild.name != "ElectricGrid")
        {
            LevelManager.main.SpendCurrency(towerToBuild.cost);
            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            turret = towerObj.GetComponent<Turret>();
            turret.setTypeOfTurret(BuildManager.main.GetTowerNumber());
            BuildManager.main.SetSelectedTower(-1);
        }
    }
}
