using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene("DemoScene package");
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    public void resurrect()
    {
        SceneManager.LoadScene("DemoScene package 1");
    }
    public void ToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
