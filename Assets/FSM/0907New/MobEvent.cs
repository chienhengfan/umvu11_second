using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobEvent : MonoBehaviour
{
    public GameObject AttackWeapon;

    private List<GameObject> AttackItems;
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
        GameObject arrow =  Instantiate(AttackWeapon,this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Debug.Log("Shoot Sth from Mob");
        if (AttackWeapon != null)
        {
            Debug.Log("Mob Shoot Arrow");

            float fArrowToPlayer = Vector3.Distance(AttackWeapon.transform.position, player.transform.position);

            //Instantiate(AttackWeapon, arrowStart.position, transform.rotation);
            AttackWeapon.transform.forward = transform.forward;
            Debug.Log("Shoot Sth");
            //iceArrow.transform.forward = arrowStart.forward;

            if (fArrowToPlayer < 0.001f)
            {
                ThirdPersonController tpc = player.GetComponent<ThirdPersonController>();
                if (tpc != null)
                {
                    Debug.Log("Player Get Hit");
                    tpc.TakeDamage(5);
                }
            }
        }
    }
}
