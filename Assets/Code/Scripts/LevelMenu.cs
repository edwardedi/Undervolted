using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void OpenMap(int mapId)
    {
        SceneManager.LoadScene(mapId);
    }
}
