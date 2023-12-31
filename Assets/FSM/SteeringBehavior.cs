﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SteeringBehavior
{
    static public void Move(AIData data)
    {
        if (data.m_bMove == false)
        {
            return;
        }
        Transform t = data.m_Go.transform;
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vR = t.right;
        Vector3 vOriF = t.forward;
        Vector3 vF = data.m_vCurrentVector;

        if(data.m_fTempTurnForce > data.m_fMaxRot)
        {
            data.m_fTempTurnForce = data.m_fMaxRot;
        } else if(data.m_fTempTurnForce < -data.m_fMaxRot)
        {
            data.m_fTempTurnForce = -data.m_fMaxRot;
        }
        
        vF = vF + vR * data.m_fTempTurnForce;
        //vF.Normalize();
        t.forward = vF;




        if (data.m_bCol == false)
        {
            if (SteeringBehavior.CheckNPCCollision(data))
            {
                Debug.Log("Check NPC Collision true");
                //t.forward = vOriF;
                if (data.m_fMoveForce < 0)
                {
                    Debug.Log("m_fMoveForce: " + data.m_fMoveForce);
                    data.m_fMoveForce = 100.0f;//-data.m_fMoveForce;
                    
                }
            }
            else
            if (SteeringBehavior.CheckCollision(data))
            {
                Debug.Log("CheckCollision true");
                t.forward = vOriF;
                if (data.m_fMoveForce < 0)
                {
                    Debug.Log("m_fMoveForce: " + data.m_fMoveForce);
                    data.m_fMoveForce = 100.0f;//-data.m_fMoveForce;
                }
            }
            else
            {
                // Debug.Log("CheckCollision true");
            }
        }
        Debug.Log(data.m_bCol + ":" + data.m_fMoveForce);
        data.m_Speed = data.m_Speed + data.m_fMoveForce;
        if (data.m_Speed < 0.1f)
        {
            data.m_Speed = 0.1f;
        }
        else if (data.m_Speed > data.m_fMaxSpeed)
        {
            data.m_Speed = data.m_fMaxSpeed;
        }
        if(data.m_bCol)
        {
            
            if (data.m_Speed < 0.11f)
            {
                if (data.m_fTimeCounter > 0.5f)
                {
                    if (data.m_fTempTurnForce > 0)
                    {
                        t.forward = vR;
                    }
                    else
                    {
                        t.forward = -vR;
                    }
                    data.m_fTimeCounter = 0;
                } else
                {
                    data.m_fTimeCounter += Time.deltaTime;
                }
            } else
            {
                data.m_fTimeCounter = 0;
            }
        } else
        {
            data.m_fTimeCounter = 0;
        }

        data.m_fMoveAmount = data.m_Speed * Time.deltaTime;
        cPos = cPos + t.forward * data.m_fMoveAmount;
        t.position = cPos;
    }

    static public bool CheckNPCCollision(AIData data)
    {
        Collider[] colliders = Physics.OverlapSphere(data.m_Go.transform.position, data.m_fRadius);
        List<Transform> enemiesAround = new List<Transform>();
        foreach(var collider in colliders)
        {
            if(collider.gameObject.CompareTag("Enemy"))
            {
                enemiesAround.Add(collider.gameObject.transform);
            }
        }
        if(enemiesAround == null)
        {
            return false;
        }
        Transform ct = data.m_Go.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        Vector3 vec;
        float fDist = 0.0f;
        float fDot = 0.0f;
        int iCount = enemiesAround.Count;
        for (int i = 0; i < iCount; i++)
        {
            vec = enemiesAround[i].position - cPos;
            fDist = Vector3.Distance(cPos, enemiesAround[i].position);
            if (fDist > 2) //小怪間距大於2
            {
                continue;
            }
            
            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0) //小怪在背面
            {
                continue;
            }
            float fProjDist = fDist * fDot;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist >  data.m_fRadius *1.5) //取得小怪半徑困難，先訂本小怪半徑1.5倍
            {
                continue;
            }
            return true;
        }
        return false;
    }
    static public bool CheckCollision(AIData data)
    {
        List<Obstacle> m_AvoidTargets = Main.m_Instance.GetObstacles();
        if (m_AvoidTargets == null)
        {
            return false;
        }
        Transform ct = data.m_Go.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        Vector3 vec;

        float fDist = 0.0f;
        float fDot = 0.0f;
        int iCount = m_AvoidTargets.Count;
        for (int i = 0; i < iCount; i++)
        {
            vec = m_AvoidTargets[i].transform.position - cPos;
            vec.y = 0.0f;
            fDist = vec.magnitude;
            if (fDist > data.m_fProbeLength + m_AvoidTargets[i].m_fRadius)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }

            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }
            m_AvoidTargets[i].m_eState = Obstacle.eState.INSIDE_TEST;
            float fProjDist = fDist * fDot;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist > m_AvoidTargets[i].m_fRadius + data.m_fRadius)
            {
                continue;
            }

            return true;


        }
        return false;
    }

    static public bool NPCCollisionAvoid(AIData data)
    {
        Collider[] colliders = Physics.OverlapSphere(data.m_Go.transform.position, data.m_fRadius);
        List<Transform> enemiesAround = new List<Transform>();
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                enemiesAround.Add(collider.gameObject.transform);
            }
        }
        if (enemiesAround == null)
        {
            return false;
        }
        Transform ct = data.m_Go.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        data.m_vCurrentVector = cForward;
        Vector3 vec;
        float fFinalDotDist;
        float fFinalProjDist;
        Vector3 vFinalVec = Vector3.forward;
        Transform oFinal = null;
        float fFinalRadius = -1.0f;
        float fDist = 0.0f;
        float fDot = 0.0f;
        float fFinalDot = 0.0f;
        int iCount = enemiesAround.Count;

        float fMinDist = 10000.0f;
        for (int i = 0; i < iCount; i++)
        {
            vec = enemiesAround[i].transform.position - cPos;
            fDist = vec.magnitude;
            if (fDist > 2.0f) //小怪間距大於2
            {
                continue;
            }

            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0) //小怪在背面
            {
                continue;
            }
            float fProjDist = fDist * fDot;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist > data.m_fRadius * 1.5f) //取得小怪半徑困難，先訂本小怪半徑1.5倍
            {
                continue;
            }

            if (fDist < fMinDist)
            {
                fMinDist = fDist;
                fFinalDotDist = fDotDist;
                fFinalProjDist = fProjDist;
                vFinalVec = vec;
                fFinalRadius = 1.0f; //假設被偵測到小怪半徑
                oFinal = enemiesAround[i].transform;

                fFinalDot = fDot;
            }

        }


        if (oFinal != null)
        {
            Vector3 vCross = Vector3.Cross(cForward, vFinalVec);
            float fTurnMag = 1.0f - Mathf.Sqrt(1.0f - fFinalDot * fFinalDot);
            if (vCross.y > 0.0f)
            {
                fTurnMag = -fTurnMag;
            }
            data.m_fTempTurnForce = fTurnMag;

            float fTotalLen = data.m_fProbeLength + fFinalRadius;
            float fRatio = fMinDist / fTotalLen;
            if (fRatio > 1.0f)
            {
                fRatio = 1.0f;
            }
            fRatio = 1.0f - fRatio;
            data.m_fMoveForce = -fRatio;
            // oFinal.m_eState = Obstacle.eState.COL_TEST;
            data.m_bCol = true;
            data.m_bMove = true;
            return true;
        }
        data.m_bCol = false;
        return false;
    }

    static public bool CollisionAvoid(AIData data)
    {
        List<Obstacle> m_AvoidTargets = Main.m_Instance.GetObstacles();
        Transform ct = data.m_Go.transform;
        Vector3 cPos = ct.position;
        Vector3 cForward = ct.forward;
        data.m_vCurrentVector = cForward;
        Vector3 vec;
        float fFinalDotDist;
        float fFinalProjDist;
        Vector3 vFinalVec = Vector3.forward;
        Transform oFinal = null;
        float fFinalRadius = -1.0f;
        float fDist = 0.0f;
        float fDot = 0.0f;
        float fFinalDot = 0.0f;
        int iCount = m_AvoidTargets.Count;

        float fMinDist = 10000.0f;
        for (int i = 0; i < iCount; i++)
        {
            vec = m_AvoidTargets[i].transform.position - cPos;
            vec.y = 0.0f;
            fDist = vec.magnitude;
            if (fDist > data.m_fProbeLength + m_AvoidTargets[i].m_fRadius)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            }

            vec.Normalize();
            fDot = Vector3.Dot(vec, cForward);
            if (fDot < 0)
            {
                m_AvoidTargets[i].m_eState = Obstacle.eState.OUTSIDE_TEST;
                continue;
            } else if(fDot > 1.0f)
            {
                fDot = 1.0f;
            }
            m_AvoidTargets[i].m_eState = Obstacle.eState.INSIDE_TEST;
            float fProjDist = fDist * fDot;
            float fDotDist = Mathf.Sqrt(fDist * fDist - fProjDist * fProjDist);
            if (fDotDist > m_AvoidTargets[i].m_fRadius + data.m_fRadius)
            {
                continue;
            }

            if (fDist < fMinDist)
            {
                fMinDist = fDist;
                fFinalDotDist = fDotDist;
                fFinalProjDist = fProjDist;
                vFinalVec = vec;
                fFinalRadius = m_AvoidTargets[i].m_fRadius;
                oFinal = m_AvoidTargets[i].transform;
  
                fFinalDot = fDot;
            }

        }


        if (oFinal != null)
        {
            Vector3 vCross = Vector3.Cross(cForward, vFinalVec);
            float fTurnMag = 1.0f - Mathf.Sqrt(1.0f - fFinalDot * fFinalDot);
            if (vCross.y > 0.0f)
            {
                fTurnMag = -fTurnMag;
            }
            data.m_fTempTurnForce = fTurnMag;

            float fTotalLen = data.m_fProbeLength + fFinalRadius;
            float fRatio = fMinDist / fTotalLen;
            if(fRatio > 1.0f)
            {
                fRatio = 1.0f;
            }
            fRatio = 1.0f - fRatio;
            data.m_fMoveForce = -fRatio;
           // oFinal.m_eState = Obstacle.eState.COL_TEST;
            data.m_bCol = true;
            data.m_bMove = true;
            return true;
        }
        data.m_bCol = false;
        return false;
    }

    static public bool SeekDirection(AIData data)
    {
        Vector3 vec = data.m_vTargetVector;
        Vector3 vf = data.m_Go.transform.forward;
        Vector3 vr = data.m_Go.transform.right;
        data.m_vCurrentVector = vf;
        vec.Normalize();
        float fDotF = Vector3.Dot(vf, vec);
        if (fDotF > 0.96f)
        {
            fDotF = 1.0f;
            data.m_vCurrentVector = vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else
        {
            if (fDotF < -1.0f)
            {
                fDotF = -1.0f;
            }
            float fDotR = Vector3.Dot(vr, vec);

            if (fDotF < 0.0f)
            {

                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }

            }
            data.m_fTempTurnForce = fDotR;

        }
        float fMoveMul = 100.0f;
 
        data.m_fMoveForce = fDotF * fMoveMul;
        data.m_bMove = true;
        return true;
    }

    static public bool Flee(AIData data)
    {
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        data.m_fTempTurnForce = 0.0f;
        if (data.m_fProbeLength < fDist)
        {
            if(data.m_Speed > 0.01f)
            {
                data.m_fMoveForce = -1.0f;
            } 
            
            data.m_bMove = true;
            return false;
        }
        vec.Normalize();
        data.m_vTargetVector = -vec;
        return SteeringBehavior.SeekDirection(data);
        /*
        Vector3 vf = data.m_Go.transform.forward;
        Vector3 vr = data.m_Go.transform.right;
        data.m_vCurrentVector = vf;
       
        float fDotF = Vector3.Dot(vf, vec);
        if (fDotF < -0.96f)
        {
            fDotF = -1.0f;
            data.m_vCurrentVector = -vec;
            //  data.m_Go.transform.forward = -vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else
        {
            if (fDotF > 1.0f)
            {
                fDotF = 1.0f;
            }
            float fDotR = Vector3.Dot(vr, vec);

            if (fDotF > 0.0f)
            {

                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }

            }
            Debug.Log(fDotR);
            data.m_fTempTurnForce = -fDotR;

            // data.m_fTempTurnForce *= 0.1f;


        }

        data.m_fMoveForce = -fDotF;
        data.m_bMove = true;
        return true;
        */
    }
    static public bool Chase(AIData data)
    {
        float timeValue = 60.0f;
        Transform targetTr = data.m_ChaseTarget.m_Go.transform;
        Vector3 target = targetTr.position + targetTr.forward * data.m_ChaseTarget.m_Speed* timeValue;
        Vector3 cPos = data.m_Go.transform.position;
        data.m_ChaseTarget.m_vTarget = target;
        Vector3 vec = target - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        if (fDist < data.m_fMoveAmount + 0.001f)
        {
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_Speed = 0.0f;
            return true;
        }
        vec.Normalize();
        data.m_vTargetVector = vec;
        SteeringBehavior.SeekDirection(data);
        if (fDist < 3.0f)
        {
            data.m_fTempTurnForce *= (2.0f - fDist / 3.0f);
        }
        return true;
    }



    static public bool SeekEnemy(AIData data)
    {
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        //float rand = Random.Range(0.6f, 1.0f);
        //float randProbe = data.m_fProbeLength * rand;
        if (fDist < data.m_fMoveAmount + 0.001f)
        {
            Vector3 vFinal = data.m_vTarget;
            vFinal.y = cPos.y;
            data.m_Go.transform.position = vFinal;
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_Speed = 0.0f;
            data.m_bMove = false;
            return false;
        }
        vec.Normalize();
        data.m_vTargetVector = vec;
        SteeringBehavior.SeekDirection(data);
        float fDeaccDist = 0.1f;
        if (fDist < 3.0f)
        {
            data.m_fTempTurnForce *= (2.0f - fDist / 3.0f);
        }

        if (fDist < fDeaccDist)
        {
            Debug.Log(data.m_Speed);
            if (data.m_Speed > 0.01f)
            {
                data.m_fMoveForce = -(1.0f - fDist / fDeaccDist) * 10.0f;
            }
        }
        return true;

        /* Vector3 vf = data.m_Go.transform.forward;
         Vector3 vr = data.m_Go.transform.right;
         data.m_vCurrentVector = vf;

         float fDotF = Vector3.Dot(vf, vec);
         if(fDotF > 0.96f)
         {
             fDotF = 1.0f;
             data.m_vCurrentVector = vec;
             data.m_fTempTurnForce = 0.0f;
             data.m_fRot = 0.0f;
         } else
         {
             if (fDotF < -1.0f)
             {
                 fDotF = -1.0f;
             }
             float fDotR = Vector3.Dot(vr, vec);

             if (fDotF < 0.0f)
             {

                 if (fDotR > 0.0f)
                 {
                     fDotR = 1.0f;
                 } else
                 {
                     fDotR = -1.0f;
                 }

             } 
             if(fDist < 3.0f)
             {
                 fDotR *= (2.0f - fDist / 3.0f);
             }
             data.m_fTempTurnForce = fDotR;

         }
         float fDeaccDist = 10.0f;
         float fMoveMul = 10.0f;
         if(fDist < fDeaccDist)
         {
             Debug.Log(data.m_Speed);
             if(data.m_Speed > 0.01f)
             {
                 data.m_fMoveForce = -(1.0f - fDist/ fDeaccDist) *1000.0f;
             } else
             {
                 data.m_fMoveForce = fDotF* fMoveMul;
             }

         } else
         {
             data.m_fMoveForce = fDotF*fMoveMul;
         }


         Debug.Log(data.m_fMoveForce + ":" + data.m_Speed);
         data.m_bMove = true;
         return true;*/
    }static public bool Seek(AIData data)
    {
        Vector3 cPos = data.m_Go.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        float rand = Random.Range(0.6f, 1.0f);
        float randProbe = data.m_fProbeLength * rand;
        if (fDist < randProbe)
        {
            //if enemy are close enough to the target, stop the move
            //Vector3 vFinal = data.m_vTarget;
            //vFinal.y = cPos.y;
            //data.m_Go.transform.position = vFinal;
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_Speed = 0.0f;
            data.m_bMove = false;
            return false;
        }
        vec.Normalize();
        data.m_vTargetVector = vec;
        SteeringBehavior.SeekDirection(data);
        float fDeaccDist = 0.1f;
        if (fDist < 3.0f)
        {
            data.m_fTempTurnForce *= (2.0f - fDist / 3.0f);
        }

        if (fDist < fDeaccDist)
        {
            Debug.Log(data.m_Speed);
            if (data.m_Speed > 0.01f)
            {
                data.m_fMoveForce = -(1.0f - fDist / fDeaccDist) * 10.0f;
            }
        }
        return true;

        /* Vector3 vf = data.m_Go.transform.forward;
         Vector3 vr = data.m_Go.transform.right;
         data.m_vCurrentVector = vf;

         float fDotF = Vector3.Dot(vf, vec);
         if(fDotF > 0.96f)
         {
             fDotF = 1.0f;
             data.m_vCurrentVector = vec;
             data.m_fTempTurnForce = 0.0f;
             data.m_fRot = 0.0f;
         } else
         {
             if (fDotF < -1.0f)
             {
                 fDotF = -1.0f;
             }
             float fDotR = Vector3.Dot(vr, vec);

             if (fDotF < 0.0f)
             {

                 if (fDotR > 0.0f)
                 {
                     fDotR = 1.0f;
                 } else
                 {
                     fDotR = -1.0f;
                 }

             } 
             if(fDist < 3.0f)
             {
                 fDotR *= (2.0f - fDist / 3.0f);
             }
             data.m_fTempTurnForce = fDotR;

         }
         float fDeaccDist = 10.0f;
         float fMoveMul = 10.0f;
         if(fDist < fDeaccDist)
         {
             Debug.Log(data.m_Speed);
             if(data.m_Speed > 0.01f)
             {
                 data.m_fMoveForce = -(1.0f - fDist/ fDeaccDist) *1000.0f;
             } else
             {
                 data.m_fMoveForce = fDotF* fMoveMul;
             }

         } else
         {
             data.m_fMoveForce = fDotF*fMoveMul;
         }


         Debug.Log(data.m_fMoveForce + ":" + data.m_Speed);
         data.m_bMove = true;
         return true;*/
    }
}
