using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject Endmenu;

    private void Start()
    {
        Endmenu.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Endmenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
