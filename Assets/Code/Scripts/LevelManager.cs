using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform secondStartPoint;
    public Transform[] path;

    public int currency;
    public int electricity;
    public int life;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100000;
        electricity = 5;
        life = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GenerateElectricity(int amount)
    {
        electricity += amount;
    }

    public bool ConsumeElectricity(int amount)
    {
        if (amount <= electricity)
        {
            electricity -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckElectricity(int amount)
    {
        if (amount <= electricity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool decreaseLife()
    {
        if (life > 0)
        {
            life--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
