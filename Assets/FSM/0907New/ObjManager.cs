using MagicaCloth2;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    private  static List<GameObject> activeOjects = new List<GameObject>();
    private static List<GameObject> inactiveObj = new List<GameObject>();
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("activeOjectsCount: " + activeOjects.Count);
        if (activeOjects.Count > 0)
        {
            Debug.Log("activeOjectsCount>0");
            foreach (GameObject obj in activeOjects)
            {
                
                obj.transform.Translate(5 * Time.deltaTime * obj.transform.forward); // 5是弓箭速度
            }
        }
    }

    public static void ActivateArrow(GameObject arrow, Vector3 startPos, Vector3 targetPos)
    {
        
        if (inactiveObj.Contains(arrow))
        {
            Debug.Log("arrowIninactiveObjList");
            GameObject obj = arrow;
            obj.transform.position = startPos;
            Vector3 vecToTar = targetPos - startPos;
            inactiveObj.Remove(obj);
            activeOjects.Add(obj);
            obj.SetActive(true);
        }
        else
        {
            Debug.Log("Dont have arrow");
            //Need To Instantiate arrow?
        }
    }
    public static void InactiveArrow(GameObject arrow)
    {
        if (activeOjects.Contains(arrow))
        {
            arrow.SetActive(false);
            GameObject go = arrow;
            activeOjects.Remove(arrow);
            inactiveObj.Add(go);
        }
    }


    /// <summary>
    /// 外部製作的GameObject加入到物件池
    /// </summary>
    /// <param name="arrow"></param>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    public static void AddToList(GameObject arrow)
    {
        arrow.SetActive(false);
        inactiveObj.Add(arrow);
    }
    public List<GameObject> GetActiveList()
    {
        return activeOjects;
    }
}
