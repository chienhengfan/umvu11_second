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
            AudioSource bossBGM = BossBGM.GetComponent<AudioSource>();
            bossBGM.volume -= Time.deltaTime * 0.1f;
            if (bossBGM.volume < 0)
            {
                bossBGM.volume = 0;
            }
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
