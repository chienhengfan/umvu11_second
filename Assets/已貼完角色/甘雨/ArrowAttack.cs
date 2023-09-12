using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArrowAttack : MonoBehaviour
{
    //public GameObject arrowStart;

    private float lifeTime = 2.0f;
    private float currentTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //arrowStart = GameObject.Find("bowStart");
        //transform.forward = GameObject.FindGameObjectWithTag("Player").transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            
            Destroy(gameObject);
            currentTime = 0;
        }
        transform.position += transform.forward * 20 * Time.deltaTime;
    }
}
