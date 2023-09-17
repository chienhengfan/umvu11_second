using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BossEvent : MonoBehaviour
{

    public ParticleSystem energyBall;
    public Vector3 vMove;

    private Transform shootStart;
    private ParticleSystem[] EnergyBalls = null;
    private int ballNum = 10;
    private int currentBallNum = 0;

    public float directAttackRange = 4f;

    public ThirdPersonController playerScript;
    private void Start()
    {


        EnergyBalls = new ParticleSystem[ballNum];

        for (int i = 0; i < EnergyBalls.Length; i++)
        {
            EnergyBalls[i] = Instantiate(energyBall);
            EnergyBalls[i].gameObject.SetActive(false);
        }
    }



    void EnergyBallControlEvent()
    {
        shootStart = gameObject.transform.Find("ShootStart");
        //Debug.Log("Event Ball");
        currentBallNum = currentBallNum % ballNum;
        EnergyBalls[currentBallNum].gameObject.transform.position = shootStart.position;
        EnergyBalls[currentBallNum].gameObject.transform.forward = shootStart.forward;
        EnergyBalls[currentBallNum].gameObject.SetActive(true);

        currentBallNum++;
    }

    void DirectAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < directAttackRange)
        {
            playerScript.TakeDamage(10f);
        }
    }
}
