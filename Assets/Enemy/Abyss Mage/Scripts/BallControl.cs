using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class BallControl : MonoBehaviour
{
    private int ballSpeed = 1;
    private float fMultiplier = 10.0f;

    private Transform playerT;
    private float ballDropTime = 0.0f;
    private float fLifeTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vFOr = transform.forward;
        Vector3 vToP = playerT.position - transform.position;
        vFOr = Vector3.Lerp(vFOr, vToP, 0.1f);
        if(vToP.magnitude < 0.1f)
        {
            gameObject.SetActive(false);
        }
        if(ballDropTime >= fLifeTime)
        {
            ballDropTime = 0.0f;
            gameObject.SetActive(false);
        }
        ballDropTime += Time.deltaTime;
        Debug.Log("ballDropTime: " + ballDropTime);
        //vToP.y -= ballDropTime;
        //vToP = vToP + vFOr;
        vToP.Normalize();
        transform.forward = vFOr;
        transform.position =transform.position + vFOr * Time.deltaTime * fMultiplier;
    }
}
