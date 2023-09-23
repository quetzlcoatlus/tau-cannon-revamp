using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Sandbox Level");
    }

    public void Level()
    {
        SceneManager.LoadScene("LevelSelect1");
    }

    public void Settings()
    {
        SceneManager.LoadScene("MenuSettings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("MenuCredits");
    }

    public void Back()
    {
        SceneManager.LoadScene("MenuMain");
    }
}
