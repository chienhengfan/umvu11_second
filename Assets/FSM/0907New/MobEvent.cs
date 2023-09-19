using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobEvent : MonoBehaviour
{
    public GameObject AttackWeapon;

    private List<GameObject> AttackItems;
    private List<GameObject> arrowList;
    private int weaponNum = 10;
    private int shootwpNum = 0;
    ObjManager objM = new ObjManager();
    private GameObject player;
    private Transform tShootStart;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform[] trs = this.GetComponentsInChildren<Transform>();
        foreach (Transform t in trs)
        {
            if (t != null)
            {
                if (t.gameObject.name == "bowFront")
                {
                    Debug.Log("bowFront" +  t.gameObject.name);
                    tShootStart = t;
                }
            }
        }
        arrowList = new List<GameObject>();
        for (int i = 0; i < weaponNum; i++)
        {
            GameObject arrow = Instantiate(AttackWeapon, transform.position, Quaternion.identity);
            arrow.SetActive(false);
            //objM.AddToList(arrow);
            arrowList.Add(arrow);
        }
        Debug.Log("arrowListCount: " + arrowList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        shootwpNum = shootwpNum % weaponNum;

        if (arrowList.Count > 0)
        {
            Debug.Log("Get arrowList");
            if (tShootStart == null)
            {
                Debug.Log("Dont have shootStart");
                return;
            }
            GameObject curArrow = arrowList[shootwpNum];
            curArrow.transform.position = tShootStart.transform.position;
            Vector3 vPlayer = player.transform.position + player.transform.up*1.0f;
            Vector3 vFor = vPlayer - tShootStart.transform.position;
            curArrow.transform.forward = vFor;
            curArrow.SetActive(true);

            //objM.ActivateArrow(curArrow, tShootStart.position, vPlayer);
            
        }
        shootwpNum++;
    }
}
