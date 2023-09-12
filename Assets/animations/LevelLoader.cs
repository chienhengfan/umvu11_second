using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    private int  num = 0;
    public float transitionTime = 1f;
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            transition.SetTrigger("Start");
            SceneManager.LoadScene(1);
            StartCoroutine(LoadLevel(1));
           
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
        num++;
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
