using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    private float speed = 10f;


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= gameObject.transform.up * speed * Time.deltaTime;
        
    }
}
