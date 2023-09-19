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
            GameObject arrow = Instantiate(AttackWeapon, this.transform);
            arrow.SetActive(false);
            ObjManager.AddToList(arrow);
            arrowList.Add(arrow);
        }

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
            ObjManager.ActivateArrow(curArrow, tShootStart.position, player.transform.position);
            Debug.Log("playerPos: " + player.transform.position);
        }
        shootwpNum++;
    }
}
