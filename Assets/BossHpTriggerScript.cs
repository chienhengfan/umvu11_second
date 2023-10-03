using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpTriggerScript : MonoBehaviour
{
    private bool bossHpTrig = false;
    public GameObject bossHp;
    public GameObject boss;
    public GameObject BossBGM;


    private void Update()
    {
        if (!boss.activeSelf)
        {
            bossHp.SetActive(false);
            BossBGM.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (bossHpTrig == false)
        {
            if (other.CompareTag("Player"))
            {
                bossHp.SetActive(true);
                bossHpTrig = true;
                BossBGM.GetComponent<AudioSource>().Play();
            }
        }
    }

}
