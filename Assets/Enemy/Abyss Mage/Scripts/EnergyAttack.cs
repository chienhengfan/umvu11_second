using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAttack : MonoBehaviour
{
    public ParticleSystem energyBall;
    public Vector3 vMove;

    private Transform shootStart;
    private ParticleSystem[] EnergyBalls = null;
    private int ballNum = 10;
    private int currentBallNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnergyBalls = new ParticleSystem[ballNum];

        for (int i = 0; i < EnergyBalls.Length; i++)
        {
            EnergyBalls[i] = Instantiate(energyBall);
            EnergyBalls[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnergyBallControlEvent()
    {
        shootStart = gameObject.transform.Find("ShootStart");
        Debug.Log("Event Ball");
        currentBallNum = currentBallNum % ballNum;
        EnergyBalls[currentBallNum].gameObject.transform.position = shootStart.position;
        EnergyBalls[currentBallNum].gameObject.transform.forward = shootStart.forward;
        EnergyBalls[currentBallNum].gameObject.SetActive(true);
        
        currentBallNum++;
    }


}
