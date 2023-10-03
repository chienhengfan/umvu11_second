using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject Endmenu;
    public bool isend = false;
    private void Start()
    {
        Endmenu.SetActive(false);
    }

    private void Update()
    {
        if(isend == true && !Endmenu.activeSelf)
        {
            Endmenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isend = true;

        }
        
    }
}
