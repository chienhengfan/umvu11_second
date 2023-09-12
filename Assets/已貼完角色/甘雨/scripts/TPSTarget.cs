using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSTarget : MonoBehaviour
{
    public Transform targetTo;
    public float height;
    Vector3 currentVel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateTarget()
    {
        Vector3 vTo = targetTo.position + Vector3.up * height;
        transform.position = Vector3.SmoothDamp(transform.position, vTo, ref currentVel, 0.3f);
       // transform.position = Vector3.Lerp(transform.position, vTo, 0.01f);
    }
}
