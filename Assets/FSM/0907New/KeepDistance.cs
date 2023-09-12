using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    private List<GameObject> enemyAround = new List<GameObject>();

    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        float fDist = 50.0f;
        GameObject[] allenemy = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (allenemy != null || allenemy.Length > 0)
        {
            foreach (GameObject go in allenemy)
            {
                Vector3 vDis = go.transform.position - transform.position;
                if(vDis.magnitude <= fDist)
                {
                    enemyAround.Add(go);
                }
            }
            enemyAround.Remove(this.gameObject);
        }
        radius = this.GetComponent<SimpleFSM2>().m_Data.m_fRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemyAround != null && enemyAround.Count > 0)
        {
            foreach (var go in enemyAround)
            {

                Vector3 vDis = go.transform.position - transform.position;
                float fDis = vDis.magnitude;
                vDis.Normalize();
                float otherR = go.GetComponent<SimpleFSM2>().m_Data.m_fRadius;

                if (fDis < radius + otherR)
                {
                    Vector3 keepPos = this.transform.position + vDis * (radius + otherR);
                    go.transform.position = keepPos;
                }
            }
        }
    }
}
