using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void GoToLevel(int level)
    {
        SceneManager.LoadScene($"Level{level}");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credicts");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
