using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using StarterAssets;

public class BallControl : MonoBehaviour
{
    public LayerMask mask;
    public float radius = 1.0f;
    public ParticleSystem firePlane;
    public ParticleSystem explosion;

    private float ballSpeed = 5.0f;
    public ThirdPersonController playerScript;
    private Transform playerT;
    private float ballDropTime = 0.0f;
    private float fLifeTime = 2.0f;
    private float fPassTime = 2.0f;
    float fScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vFOr = transform.forward;
        Vector3 vPlayerChasePoint = playerT.transform.position + playerT.transform.up * 1.0f;
        Vector3 vToP = vPlayerChasePoint - transform.position;
        vFOr = Vector3.Lerp(vFOr, vToP, 0.1f);
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, mask);
        if (cols.Length > 0)
        {
            //隔固定秒數加上firePlane特效
            foreach (Collider col in cols)
            {
                //Vector3 colPoint = Vector3.zero;
                //float f = (fPassTime / 0.2f);
                //if(fPassTime > 1.0f)
                //{
                //    colPoint = new Vector3(transform.position.x, playerT.transform.position.y, transform.position.z);
                //    Debug.Log("fireballHitColPoint: " + colPoint);
                //    if (firePlane != null)
                //    {
                //        ParticleSystem fPlane = Instantiate(firePlane, colPoint, Quaternion.identity);
                //        fPlane.transform.localScale = Vector3.one * fScale;
                //    }
                //}
                //else if (fPassTime > 0.8f)
                //{
                //    colPoint = new Vector3(transform.position.x, playerT.transform.position.y, transform.position.z);
                //    Debug.Log("fireballHitColPoint: " + colPoint);
                //    if (firePlane != null)
                //    {
                //        ParticleSystem fPlane = Instantiate(firePlane, colPoint, Quaternion.identity);
                //        fPlane.transform.localScale = Vector3.one * fScale;
                //    }
                //}
                //else if (fPassTime > 0.6f)
                //{
                //    colPoint = new Vector3(transform.position.x, playerT.transform.position.y, transform.position.z);
                //    Debug.Log("fireballHitColPoint: " + colPoint);
                //    if (firePlane != null)
                //    {
                //        ParticleSystem fPlane = Instantiate(firePlane, colPoint, Quaternion.identity);
                //        fPlane.transform.localScale = Vector3.one * fScale;
                //    }
                //}
                //else if (fPassTime > 0.4f)
                //{
                //    colPoint = new Vector3(transform.position.x, playerT.transform.position.y, transform.position.z);
                //    Debug.Log("fireballHitColPoint: " + colPoint);
                //    if (firePlane != null)
                //    {
                //        ParticleSystem fPlane = Instantiate(firePlane, colPoint, Quaternion.identity);
                //        fPlane.transform.localScale = Vector3.one * fScale;
                //    }
                //}
                //else if (fPassTime > 0.2f)
                //{
                //    colPoint = new Vector3(transform.position.x, playerT.transform.position.y, transform.position.z);
                //    Debug.Log("fireballHitColPoint: " + colPoint);
                //    if (firePlane != null)
                //    {
                //        ParticleSystem fPlane = Instantiate(firePlane, colPoint, Quaternion.identity);
                //        fPlane.transform.localScale = Vector3.one * fScale;
                //    }
                //}
                //fScale += f;
                //fPassTime += Time.deltaTime;
            }
        }
        if(vToP.magnitude < 0.1f )
        {
            Debug.Log("FireBallHit Sth");
            //火球碰到玩家爆炸
            Instantiate(explosion, transform.position, Quaternion.identity);
            playerScript.TakeDamage(10);
            Debug.Log("explosionInstance: " +explosion.transform.position);
            gameObject.SetActive(false);
        }
        if(ballDropTime >= fLifeTime)
        {
            ballDropTime = 0.0f;
            gameObject.SetActive(false);
        }
        ballDropTime += Time.deltaTime;
        
        //Make ball Drop
        vToP.y -= Time.deltaTime;
        vToP = vToP + vFOr;
        vToP.Normalize();
        transform.forward = vToP;
        transform.position =transform.position + vToP * Time.deltaTime * ballSpeed;
    }

    private void OnDrawGizmos()
    {
    }
}
