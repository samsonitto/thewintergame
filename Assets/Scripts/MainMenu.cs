using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool menuIsEnabled = false;

    void OnEnable()
    {
        menuIsEnabled = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        menuIsEnabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
