using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class KeepDistance : MonoBehaviour
{
    private List<GameObject> enemyAround = new List<GameObject>();

    private float targetVelocity = 10.0f;
    private int numofRay = 8;
    private float angle = 90;
    private float rayRange = 2;
    Vector3 hitVec;
    private SimpleFSM2 sfm;

    private float radius;
    private float yPos;
    private Transform tCrossbowFront;
    private Transform tCrossbowEnd;
    public GameObject crossbowObj;

    LayerMask unwalkLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        unwalkLayerMask = LayerMask.GetMask("UnwalkLayer");
        Debug.Log("unwalkLayerMaskValue:  " +unwalkLayerMask);
        

        yPos = gameObject.transform.position.y;
        sfm = this.GetComponent<SimpleFSM2>();
        radius = this.GetComponent<SimpleFSM2>().m_Data.m_fRadius;

        GameObject[] allenemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (allenemy != null || allenemy.Length > 0)
        {
            foreach (GameObject go in allenemy)
            {
                if (go.GetComponent<SimpleFSM2>() != null)
                {
                    enemyAround.Add(go);
                }
            }
            enemyAround.Remove(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (crossbowObj != null)
        {
            Transform bowEnd = transform.Find("ダミー_r");
            Vector3 vE = bowEnd.position;

            Transform bowFront = transform.Find("ダミー_l");
            Vector3 vF = bowEnd.position;
            Vector3 vBow = vF - vE;
            Vector3 bowFor = -crossbowObj.transform.right;
            bowFor = vBow;
        }

        for (int i = 0; i < numofRay; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (((float)numofRay) - 1)) * angle * 2, this.transform.up);
            var direction = rotation * rotationMod * transform.forward;

            var ray = new Ray(this.transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {

                if (1 << hitInfo.collider.gameObject.layer == unwalkLayerMask)
                {
                    hitVec = hitInfo.normal; // hitVec is normal vec
                    Vector3 vToCol = hitInfo.collider.gameObject.transform.position - transform.position; //vToCol 往碰撞體中心
                    vToCol.y = 0;
                    float fDis = vToCol.magnitude;
                    vToCol.Normalize();
                    Vector3 vFor = transform.forward;
                    float fColWay = Vector3.Dot(vFor, vToCol); //判斷是否朝向碰撞體
                    float fDot = Vector3.Dot(vFor, hitVec);
                    Vector3 vTurn = hitVec * fDot;
                    Vector3 vFinal = vFor + vTurn;
                    Vector3 vFinalMove = vFinal * Time.deltaTime;
                    transform.position -= vFinalMove;

                    //if (fDot > 0)
                    //{

                    //}
                    //else if (fDis > 0)
                    //{

                    //}

                }
            }
        }

        Vector3 keepPos = this.transform.position;
        keepPos.y = yPos;
        transform.position = keepPos;

        if (enemyAround != null && enemyAround.Count > 0)
        {
            foreach (var go in enemyAround)
            {
                Debug.Log("EnemyaroundName: " + go.name);
                Vector3 vDis = go.transform.position - transform.position;
                float fDis = vDis.magnitude;
                vDis.Normalize();
                float otherR = go.GetComponent<SimpleFSM2>().m_Data.m_fRadius;

                if (fDis < radius + otherR +0.1f)
                {
                    keepPos = this.transform.position + vDis * (radius + otherR + 0.1f);
                    keepPos.y = yPos;
                    go.transform.position = keepPos;
                }
            }
        }
        
    }

    

    private void OnDrawGizmos()
    {
        

        for (int i = 0; i < numofRay; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (((float)numofRay) - 1)) * angle * 2 , this.transform.up);
            var direction = rotation * rotationMod * transform.forward;
            Gizmos.DrawRay(this.transform.position, direction *2);

            var ray = new Ray(this.transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {
                
                if (1<< hitInfo.collider.gameObject.layer == unwalkLayerMask)
                {
                    Debug.Log("hitInfolayer: " + hitInfo.collider.gameObject.layer);
                    Gizmos.DrawSphere(transform.position, rayRange);
                }
            }
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 2);

        if (hitVec != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, hitVec * 5);
        }
    }

}
