using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTower = -1;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        if (selectedTower == -1)
        {
            return null;
        }
        return towers[selectedTower];
    }

    public int GetTowerNumber()
    {
        return selectedTower;
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
