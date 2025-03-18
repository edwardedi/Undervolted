using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;

    private bool isHoveringUi;

    private void Awake()
    {
        main = this;
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUi = state;
    }

    public bool IsHoveringUi()
    {
        return isHoveringUi;
    }
}
