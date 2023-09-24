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
            if (this.name == "IceArrow")
            {
                Destroy(gameObject);
                currentTime = 0;
            }
            this.gameObject.SetActive(true);
            currentTime = 0;
        }

        //Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);
        //foreach (Collider col in colliders)
        //{
        //    if (col.gameObject.CompareTag("Player"))
        //    {

        //    }
        //}
        transform.position += transform.forward  *20* Time.deltaTime;
    }
}
