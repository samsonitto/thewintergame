using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    void OnEnable()
    {
        MainMenu.menuIsEnabled = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        MainMenu.menuIsEnabled = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
