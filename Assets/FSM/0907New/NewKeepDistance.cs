using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewKeepDistance : MonoBehaviour
{

    public CharacterController myCC;
    private List<CharacterController> myEnemyControllers;
    private float myRadius = 0f;

    private void Start()
    {
        if (this.gameObject.TryGetComponent<CharacterController>(out CharacterController cc))
        {
            myRadius = cc.radius;
            Debug.Log(myRadius);
        }
        myEnemyControllers = new List<CharacterController>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            if (go.TryGetComponent<CharacterController>(out CharacterController controller))
            {
                if(controller.gameObject.name == myCC.name) { continue; } //Dont Add Myself
                myEnemyControllers.Add(controller);
            }
        }
    }

    private void Update()
    {
        foreach (CharacterController otherController in myEnemyControllers)
        {
            Vector3 otherPos = otherController.transform.position;
            Vector3 vToOther = otherPos - this.transform.position;
            float fDisSqr = vToOther.sqrMagnitude;
            float fMinDis = myRadius + otherController.radius;
            if (fDisSqr < fMinDis * fMinDis * 2f)
            {
                Debug.Log(this.gameObject.name + "Touch: " +  otherController.gameObject.name);
                float fDot = Vector3.Dot(this.transform.forward, vToOther);
                float fDotRight = Vector3.Dot(this.transform.right, vToOther);
                if (fDot >= 0f)
                {
                    //碰撞物在前側
                    if (fDotRight < 0f)
                    {
                        //碰撞物在左側
                        myCC.Move(transform.right * myRadius);
                    }
                    else
                    {
                        myCC.Move(-transform.right * myRadius);
                    }
                }
            }
        }
    }
}
