using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransport : MonoBehaviour
{
    public GameObject transPortal;
    public GameObject player;

    private bool battleDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (battleDone)
        {
            player.transform.position = transPortal.transform.position + Vector3.right * 10;
            battleDone = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Portal Trigger" + other.gameObject.name);
    //    if(other.gameObject.name == ("PlayerArmature"))
    //    {
    //        //Debug.Log("Player Transport");
    //        Debug.Log(transPortal.transform.name);
    //        SceneManager.LoadScene(1);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Portal Trigger" + other.gameObject.name);
        if (other.gameObject.name == ("PlayerArmature"))
        {
            Debug.Log("Player Transport");
            Debug.Log(transPortal.transform.name);
            battleDone = true;
            //other.gameObject.transform.position = transPortal.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Portal Trigger" + other.gameObject.name);
        if (other.gameObject.name == ("PlayerArmature"))
        {
            Debug.Log("Player Transport");
            Debug.Log(transPortal.transform.name);
            battleDone = true;
            //other.gameObject.transform.position = transPortal.transform.position;
        }
    }

}
