using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BossFSM : MonoBehaviour
{

    public enum eFSMState
    {
        NONE = -1,
        Idle,
        MoveToTarget,
        Chase,
        Attack,
        Dead
    }

    private delegate void DoState();
    private DoState m_DoState;
    private delegate void CheckAIState();
    private CheckAIState m_CheckState;

    private eFSMState m_eCurrentState;
    public AIData m_Data;
    private float m_fCurrentTime;
    private float m_fIdleTime;
    private GameObject m_CurrentEnemyTarget;
    private int m_iCurrentWanderPt;
    private GameObject[] m_WanderPoints;
    [HideInInspector]
    public Animator m_Am;


    public GameObject chaseFire;
    public GameObject meteorite;
    public ThirdPersonController playerScript;

    private int actionThreshold;

    // Use this for initialization
    public void Start()
    {
        m_CurrentEnemyTarget = null;
        m_eCurrentState = eFSMState.Idle;
        m_fCurrentTime = 0.0f;
        m_fIdleTime = Random.Range(3.0f, 5.0f);
        m_iCurrentWanderPt = -1;
        m_WanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        m_Am = GetComponent<Animator>();
        m_DoState = DoIdleState;
        m_CheckState = CheckIdleState;
        Debug.Log("First Check Idle");

    }

    private GameObject CheckEnemyInSight(ref bool bAttack)
    {
        GameObject go = Main.m_Instance.GetPlayer();
        Vector3 v = go.transform.position - this.transform.position;
        float fDist = v.magnitude;
        if (fDist < m_Data.m_fAttackRange)
        {
            bAttack = true;
            return go;
        }
        else if (fDist < m_Data.m_fSight)
        {
            bAttack = false;
            return go;
        }
        return null;
    }

    private bool CheckTargetEnemyInSight(GameObject target, ref bool bAttack)
    {
        GameObject go = target;
        Vector3 v = go.transform.position - this.transform.position;
        float fDist = v.magnitude;
        if (fDist < m_Data.m_fAttackRange)
        {
            bAttack = true;
            return true;
        }
        else if (fDist < m_Data.m_fSight)
        {
            bAttack = false;
            return true;
        }
        return false;
    }
    void CheckAttackState()
    {
        // Check Dead
        Debug.Log("Current State: " + m_eCurrentState);
        if (m_Data.m_fHp <= 0.0f)
        {
            m_Data.m_bMove = false;
            m_eCurrentState = eFSMState.Dead;
            //m_Am.SetTrigger("IsDead");
            m_DoState = DoDeadState;
            return;
        }

        bool bAttack = false;
        bool bCheck = CheckTargetEnemyInSight(m_CurrentEnemyTarget, ref bAttack);
        if (bCheck == false)
        {
            m_eCurrentState = eFSMState.Idle;
            m_fCurrentTime = 0.0f;
            m_fIdleTime = Random.Range(3.0f, 5.0f);
            m_DoState = DoIdleState;
            m_CheckState = CheckIdleState;
            return;
        }
        if (bAttack == false)
        {
            m_Data.m_TargetObject = m_CurrentEnemyTarget;
            m_eCurrentState = eFSMState.Chase;
            m_DoState = DoChaseState;
            m_CheckState = CheckChaseState;
            return;
        }
    }
    void CheckChaseState()
    {
        // Check Dead.
        Debug.Log("Current State: " + m_eCurrentState);
        if (m_Data.m_fHp <= 0.0f)
        {
            m_Data.m_bMove = false;
            m_eCurrentState = eFSMState.Dead;
            //m_Am.SetTrigger("IsDead");
            m_DoState = DoDeadState;
            return;
        }

        bool bAttack = false;
        bool bCheck = CheckTargetEnemyInSight(m_CurrentEnemyTarget, ref bAttack);
        if (bCheck == false)
        {
            m_eCurrentState = eFSMState.Idle;
            m_fCurrentTime = 0.0f;
            m_fIdleTime = Random.Range(3.0f, 5.0f);
            m_DoState = DoIdleState;
            m_CheckState = CheckIdleState;
            return;
        }
        if (bAttack)
        {
            m_eCurrentState = eFSMState.Attack;
            m_Data.m_fAttackTime = Random.Range(1.0f, 3.0f);
            m_fCurrentTime = 0.0f;
            m_DoState = DoAttackState;
            m_CheckState = CheckAttackState;
        }
    }
    void CheckMoveToState()
    {
        // Check Dead.
        Debug.Log("Current State: " + m_eCurrentState);
        if (m_Data.m_fHp <= 0.0f)
        {
            m_Data.m_bMove = false;
            m_eCurrentState = eFSMState.Dead;
            //m_Am.SetTrigger("IsDead");
            m_DoState = DoDeadState;
            return;
        }

        bool bAttack = false;
        m_CurrentEnemyTarget = CheckEnemyInSight(ref bAttack);
        if (m_CurrentEnemyTarget != null)
        {
            m_Data.m_TargetObject = m_CurrentEnemyTarget;
            if (bAttack)
            {
                m_eCurrentState = eFSMState.Attack;
                m_DoState = DoAttackState;
                m_CheckState = CheckAttackState;
            }
            else
            {
                m_eCurrentState = eFSMState.Chase;
                m_DoState = DoChaseState;
                m_CheckState = CheckChaseState;
            }
            return;
        }

        if (m_Data.m_bMove == false)
        {
            m_eCurrentState = eFSMState.Idle;
            m_fCurrentTime = 0.0f;
            m_fIdleTime = Random.Range(3.0f, 5.0f);
            m_DoState = DoIdleState;
            m_CheckState = CheckIdleState;
        }
    }
    void CheckIdleState()
    {
        // Check Dead.
        Debug.Log("Current State: " + m_eCurrentState);
        if (m_Data.m_fHp <= 0.0f)
        {
            m_Data.m_bMove = false;
            m_eCurrentState = eFSMState.Dead;
            //m_Am.SetTrigger("IsDead");
            m_DoState = DoDeadState;
            return;
        }

        bool bAttack = false;
        m_CurrentEnemyTarget = CheckEnemyInSight(ref bAttack);
        //Debug.Log("Current Enemy: " + m_CurrentEnemyTarget.name);
        if (m_CurrentEnemyTarget != null)
        {
            m_Data.m_TargetObject = m_CurrentEnemyTarget;
            if (bAttack)
            {
                m_eCurrentState = eFSMState.Attack;
                m_DoState = DoAttackState;
                m_CheckState = CheckAttackState;
            }
            else
            {
                m_eCurrentState = eFSMState.Chase;
                m_DoState = DoChaseState;
                m_CheckState = CheckChaseState;
            }
            return;
        }
        // Wait to move.
        Debug.Log(m_fCurrentTime + ":" + m_fIdleTime);
        if (m_fCurrentTime > m_fIdleTime)
        {
            m_fCurrentTime = 0.0f;
            int iNewPt = Random.Range(0, m_WanderPoints.Length);
            if (m_iCurrentWanderPt == iNewPt)
            {
                return;
            }
            m_fIdleTime = 0.5f;
            m_iCurrentWanderPt = iNewPt;
            m_Data.m_vTarget = m_WanderPoints[m_iCurrentWanderPt].transform.position;
            m_Data.m_bMove = true;
            m_eCurrentState = eFSMState.MoveToTarget;
            m_DoState = DoMoveToState;
            m_CheckState = CheckMoveToState;
        }
    }

    void CheckDeadState()
    {

    }


    void DoIdleState()
    {
        m_fCurrentTime += Time.deltaTime;
        //m_Am.SetBool("IsDead", false);
        //m_Am.SetBool("RunBool", false);
        //m_Am.SetBool("AttackBool", false);
        Debug.Log("Do Idle State");
    }
    void DoMoveToState()
    {
        if (SteeringBehavior.CollisionAvoid(m_Data) == false)
        {
            SteeringBehavior.Seek(m_Data);
        }

        SteeringBehavior.Move(m_Data);
        //m_Am.SetBool("RunBool", true);
        //m_Am.SetBool("AttackBool", false);
    }

    void DoChaseState()
    {
        m_Data.m_vTarget = m_Data.m_TargetObject.transform.position;
        if (SteeringBehavior.CollisionAvoid(m_Data) == false)
        {
            SteeringBehavior.Seek(m_Data);
        }

        SteeringBehavior.Move(m_Data);
        //m_Am.SetBool("RunBool", true);
        //m_Am.SetBool("AttackBool", false);
    }
    void DoAttackState()
    {
        if (m_fCurrentTime > m_Data.m_fAttackTime)
        {
            m_fCurrentTime = 0.0f;
            // Do attack.
        }
        m_fCurrentTime += Time.deltaTime;
        GameObject go = Main.m_Instance.GetPlayer();
        transform.LookAt(go.transform);
        //Vector3 v = go.transform.position - transform.position;
        //v.Normalize();
        //transform.forward = Vector3.Lerp(transform.forward, v, 0.1f);
        //m_Am.SetBool("RunBool", false);
        //m_Am.SetBool("AttackBool", true);
    }

    void DoDeadState()
    {
        m_Am.SetTrigger("IsDead");
        Debug.Log("Dead!!");
    }

    // Update is called once per frame
    void Update()
    {
        actionThreshold = Random.Range(0, 5);

        Debug.Log("Update State");
        m_CheckState();
        Debug.Log("m_eCurrentState: " + m_eCurrentState);
        m_DoState();


    }

    /// <summary>
    /// 追蹤火球攻擊
    /// actionThreshold = 1
    /// </summary>
    void DoChaseFireAttackState(float fireSpeed = 5f, float attackRange = 3f, float chaseTimeThreshold = 10f)
    {
        m_Am.SetFloat("ActionThreshold", actionThreshold);

        bool IsHit = false;
        float currentTime = 0f;
        GameObject player = Main.m_Instance.GetPlayer();
        GameObject chaseFie = Instantiate(chaseFire, transform.position, Quaternion.identity);
        while (!IsHit)
        {
            chaseFire.transform.position = Vector3.MoveTowards(chaseFire.transform.position, player.transform.position, fireSpeed * Time.deltaTime);
            currentTime += Time.deltaTime;
            if(Vector3.Distance(chaseFire.transform.position, player.transform.position) <= attackRange)
            {
                //player take damage
                playerScript.TakeDamage(15f);
                Destroy(chaseFie);
                IsHit = true;

            }
            else if(currentTime > chaseTimeThreshold)
            {
                Destroy(chaseFie);
                break;
            }
        }

    }

    /// <summary>
    /// 在範圍內召喚隕石攻擊
    /// actionThreshold = 0
    /// </summary>
    void DoMeteoriteAttackState(float evokeRange = 10f, int numberOfMeterite = 3, float attackRange = 3f)
    {
        m_Am.SetFloat("ActionThreshold", actionThreshold);
        GameObject player = Main.m_Instance.GetPlayer();

        for (int i = 0; i <= numberOfMeterite; i++)
        {
            GameObject m = Instantiate(meteorite, Random.insideUnitCircle * evokeRange, Quaternion.identity);
            if(Vector3.Distance(m.transform.position, player.transform.position) <= attackRange)
            {
                //player take damage
                playerScript.TakeDamage(10f);
            }
            Destroy(m);
        }
    }



    private void OnDrawGizmos()
    {
        if (m_Data == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2.0f);
        if (m_eCurrentState == eFSMState.Idle)
        {
            Gizmos.color = Color.green;
        }
        else if (m_eCurrentState == eFSMState.MoveToTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, m_Data.m_vTarget);
        }
        else if (m_eCurrentState == eFSMState.Chase)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, m_Data.m_vTarget);
        }
        else if (m_eCurrentState == eFSMState.Attack)
        {
            Gizmos.color = Color.red;
        }
        else if (m_eCurrentState == eFSMState.Dead)
        {
            Gizmos.color = Color.gray;
        }
        Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fSight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fAttackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, m_Data.m_fRadius);
    }
}
