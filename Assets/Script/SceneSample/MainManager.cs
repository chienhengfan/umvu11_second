using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance = null;
    public static MainManager Instance() { return _instance; }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
      
    }

    void FinishLoadScene(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("FinishLoadScene " + scene.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader sc = SceneLoader.Instance();
        if (sc == null)
        {
            sc = new SceneLoader();
            sc.Init();
        }
        sc.SetupLoadingCallback(FinishLoadScene);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name == "menu")
            {
                SceneLoader.Instance().ChangeScene("fps");
               // SceneManager.LoadScene("fps", LoadSceneMode.Single);
            } else
            {
                SceneLoader.Instance().ChangeScene("menu");
                //SceneManager.LoadScene("menu", LoadSceneMode.Single);
            }
        }
    }
}
