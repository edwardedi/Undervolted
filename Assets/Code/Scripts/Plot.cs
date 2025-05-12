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
        //if (UIManager.main.IsHoveringUi()) return;


        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild != null)
        {
            if (towerObj != null && towerToBuild.name != "ElectricGrid")
            {
                Debug.Log("Show upgrade UI");
                turret.openUpgradeUI();
                if (UpgradeMenu.main != null)
                    UpgradeMenu.main.SpecialUpgradeDisappear();
                FindFirstObjectByType<TurretUpgradeUI>().SetTarget(turret);
                return;
            }
        }
        else
        {
            if (towerObj != null)
            {
                Debug.Log("Show upgrade UI");
                turret.openUpgradeUI();
                if (UpgradeMenu.main != null)
                    UpgradeMenu.main.SpecialUpgradeDisappear();
                FindFirstObjectByType<TurretUpgradeUI>().SetTarget(turret);
                if (turret.levelFirstPath == 5 || turret.levelSecondPath == 5)
                    UpgradeMenu.main.SpecialUpgradeAppear();
                return;
            }
        }


        if (towerToBuild == null)
        {
            Debug.Log("Tower not selected");
            return;
        }

        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Not enough money");
            return;
        }

        if (towerToBuild.name == "ElectricGrid" && gridObj == null)
        {
            LevelManager.main.SpendCurrency(towerToBuild.cost);
            gridObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            if (towerObj != null)
                towerObj.GetComponent<Turret>().SetGridConnectivity(true);
            BuildManager.main.SetSelectedTower(-1);
            return;
        }

        if (towerToBuild.name == "Generator")
        {
            LevelManager.main.SpendCurrency(towerToBuild.cost);
            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            BuildManager.main.SetSelectedTower(4);
            gridObj = Instantiate(BuildManager.main.GetSelectedTower().prefab, transform.position, Quaternion.identity);
            BuildManager.main.SetSelectedTower(-1);
            return;
        }


        LevelManager.main.SpendCurrency(towerToBuild.cost);
        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
        turret.setTypeOfTurret(BuildManager.main.GetTowerNumber());
        BuildManager.main.SetSelectedTower(-1);

    }
}
