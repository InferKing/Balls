using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public static void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
