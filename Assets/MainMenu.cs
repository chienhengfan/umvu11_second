using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    FadeInOut fade;


    private void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    public IEnumerator SwitchScene(string scene)
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
    public void PlayGame() 
    {
        StartCoroutine(SwitchScene("DemoScene package"));
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    public void resurrect()
    {
        StartCoroutine(SwitchScene("DemoScene package 1"));
    }
    public void ToTitle()
    {
        StartCoroutine(SwitchScene("MainMenu"));
    }
}
