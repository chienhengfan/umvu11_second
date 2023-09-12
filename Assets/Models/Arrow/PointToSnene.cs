using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToSnene : MonoBehaviour
{
    private GameObject arrow;
    private bool arrowActive = true;
    private Vector3 lookFor;
    private int SceneTgNum = 0;
    public GameObject[] SceneTargets;
    // Start is called before the first frame update
    void Start()
    {
        arrow = transform.GetChild(0).gameObject;
        lookFor = SceneTargets[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Turn on/Turn off the Arrow
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    Debug.Log("Arrow false");
        //    arrow.SetActive(false);
        //    arrowActive = false;
        //}
        //if (arrowActive == false)
        //{
        //    if(Input.GetKeyDown(KeyCode.Alpha0))
        //    {
        //        Debug.Log("Arrow true");
        //        arrow.SetActive(true);
        //        arrowActive = true;
        //    }
        //}

        //Point to the SceneTargets gameObject
        if(Input.GetKeyDown(KeyCode.Alpha1) && SceneTargets.Length > 0)
        {
            SceneTgNum = SceneTgNum % SceneTargets.Length;
            Debug.Log(SceneTgNum);
            lookFor = SceneTargets[SceneTgNum].transform.position;
            SceneTgNum++;
        }
        arrow.transform.LookAt(lookFor);
        //arrow.transform.Rotate(transform.right, 90.0f);
        
        //if(SceneTargets != null || SceneTargets.Length > 0)
        //{

        //}
    }

    
}
