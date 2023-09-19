using MagicaCloth2;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    private  List<GameObject> activeOjects = new List<GameObject>();
    private List<GameObject> inactiveObj = new List<GameObject>();
    //private List<GameObject> objForMove = new List<GameObject>();
    private GameObject player;
    private float fSpeed = 10;
    private Vector3 vTarget;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("activeOjectsCount: " + activeOjects.Count);
        //讓activeObjects內物件往前移動
        if (activeOjects.Count > 0)
        {
            Debug.Log("activeOjectsCount > 0");
            foreach (GameObject obj in activeOjects)
            {
                Debug.Log("vTarget:" + vTarget + " objForward: " + obj.transform.forward);
                obj.transform.position += obj.transform.forward *fSpeed * Time.deltaTime;

                bool hitP = HitPlayer(obj);
                if (hitP)
                {
                    Debug.Log("hitPlayerInactiveObj");
                    //物件setActive(false)，物件回到inactiveObj物件池
                    InactiveArrow(obj);
                    //玩家受到傷害
                    ThirdPersonController tpc = new ThirdPersonController();
                    tpc.TakeDamage(10);
                }

            }
        }
    }

    public void ActivateArrow(GameObject arrow, Vector3 startPos, Vector3 targetPos)
    {
        
        if (inactiveObj.Contains(arrow))
        {
            Debug.Log("inactiveObjContainsarrow");
            GameObject obj = arrow;
            obj.transform.position = startPos;
            Vector3 vecToTar = targetPos - startPos;
            obj.transform.forward = vecToTar;
            //紀錄目標位置
            vTarget = targetPos;

            activeOjects.Add(obj);
            Debug.Log("activeOjectsContainsNewObj: " + activeOjects.Contains(obj) + " obj.name: " + obj.name);
            Debug.Log("activeOjectsCountobj: " + activeOjects.Count);
            inactiveObj.Remove(arrow);
            Debug.Log("inactiveObjContainsOldArrow: " + inactiveObj.Contains(arrow));
            Debug.Log("inactiveObjContainsNewObj: " + inactiveObj.Contains(obj));

            obj.SetActive(true);
        }
        else
        {
            Debug.Log("Dont have arrow");
            //Need To Instantiate arrow?
        }
    }
    private bool HitPlayer(GameObject go)
    {
        Debug.Log("CheckHitPlayer");
        Vector3 vP = player.transform.position + player.transform.up * 1.0f;
        float fDisToP = Vector3.Distance(go.transform.position, vP);
        if (fDisToP < 0.01f)
        {
            return true;
        }
        return false;
    }
    
    
    
    public void InactiveArrow(GameObject arrow)
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
    public void AddToList(GameObject arrow)
    {
        arrow.SetActive(false);
        inactiveObj.Add(arrow);
        Debug.Log("AddToInactiveList");
    }
    
}
