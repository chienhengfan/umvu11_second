using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEvent : MonoBehaviour
{
    public GameObject AttackWeapon;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Main.m_Instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Debug.Log("Shoot Sth from Mob");
        //if (AttackWeapon != null)
        //{
        //    Debug.Log("Mob Shoot Arrow");

        //    float fArrowToPlayer = Vector3.Distance(AttackWeapon.transform.position, player.transform.position);
        //    if (fArrowToPlayer < 0.001f)
        //    {
        //        ThirdPersonController tpc = player.GetComponent<ThirdPersonController>();
        //        if (tpc != null)
        //        {
        //            Debug.Log("Player Get Hit");
        //            tpc.TakeDamage(5);
        //        }
        //    }
        //}
    }
}
