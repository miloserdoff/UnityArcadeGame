using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void StartLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void StartLevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("[SUCCESS] Exit");
    }
}
