using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Used for traversing Level Select menus

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuMain");
    }

    public void LevelSelect1()
    {
        SceneManager.LoadScene("LevelSelect1");
    }

    public void LevelSelect2()
    {
        SceneManager.LoadScene("LevelSelect2");
    }

    public void LevelSelect3()
    {
        SceneManager.LoadScene("LevelSelect3");
    }

    public void LevelSelect4()
    {
        SceneManager.LoadScene("LevelSelect4");
    }

    public void LevelSelect5()
    {
        SceneManager.LoadScene("LevelSelect5");
    }

    // Used in LevelSelect1

    public void Level_0 ()
    {
        SceneManager.LoadScene("_Level0");
    }

    public void Level_1()
    {
        SceneManager.LoadScene("_Level1");
    }

    public void Level_2()
    {
        SceneManager.LoadScene("_Level2");
    }

    public void Level_3()
    {
        SceneManager.LoadScene("_Level3");
    }

    public void Level_4()
    {
        SceneManager.LoadScene("_Level4");
    }

    public void Level_5()
    {
        SceneManager.LoadScene("_Level5");
    }

    // Used in LevelSelect 2
    // need to make level scenes

    // chapters 3 4 5 coming soon?
}
