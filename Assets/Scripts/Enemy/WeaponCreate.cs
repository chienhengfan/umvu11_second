using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCreate : MonoBehaviour
{
    public ParticleSystem energyBall;
    public GameObject arrowWeapon;
    //public Vector3 vMove;

    private Transform shootStart;
    private Transform arrowStart;

    private ParticleSystem[] EnergyBalls = null;
    private GameObject[] arrowArr = null;
    private int createNum = 10;
    private int currentBallNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Mage create energyBalls
        EnergyBalls = new ParticleSystem[createNum];
        for (int i = 0; i < EnergyBalls.Length; i++)
        {
            if (energyBall == null) { continue; }
            EnergyBalls[i] = Instantiate(energyBall);
            EnergyBalls[i].gameObject.SetActive(false);
        }
        //ganyu or mob create arrows
        arrowArr = new GameObject[createNum];
        for (int i = 0; i < arrowArr.Length; i++)
        {
            if (arrowWeapon == null) { continue; }
            arrowArr[i] = Instantiate(arrowWeapon);
            arrowArr[i].gameObject.SetActive(false);
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
        currentBallNum = currentBallNum % createNum;
        EnergyBalls[currentBallNum].gameObject.transform.position = shootStart.position;
        EnergyBalls[currentBallNum].gameObject.transform.forward = shootStart.forward;
        EnergyBalls[currentBallNum].gameObject.SetActive(true);

        currentBallNum++;
    }
    void ArrowResetEvent()
    {
        if (this.name == "¥Ì«B")
        {
            arrowStart = GameObject.Find("bowStart").transform;
            Debug.Log("Event Ball");
            currentBallNum = currentBallNum % createNum;
            EnergyBalls[currentBallNum].gameObject.transform.position = arrowStart.position;
            EnergyBalls[currentBallNum].gameObject.transform.forward = arrowStart.forward;
            EnergyBalls[currentBallNum].gameObject.SetActive(true);

            currentBallNum++;
        }
        else
        {

        }

    }
    public GameObject[] GetArrowArr()
    {
        return arrowArr;
    }
}
