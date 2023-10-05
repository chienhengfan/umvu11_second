using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{

    private float lifeTime = 2.0f;
    private float currentTime = 0;
    private Transform playerT;
    private Vector3 vToPlayer;
    private float ballSpeed = 5.0f;

    [SerializeField] private Collider myCollider;

    [SerializeField] private int damage;
    //[SerializeField] private float knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void Start()
    {

    }

    void Update()
    {
        WeaponMove();
    }

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 vPlayerChasePoint = playerT.transform.position + playerT.transform.up * 1.0f; //玩家中心點
        Vector3 vToPlayer = vPlayerChasePoint - transform.position;
    }

    private void PhysicsCollide()
    {
        Vector3 bocCenter = transform.position - transform.forward * 0.5f;
        Debug.DrawLine(transform.position, bocCenter);
        Collider[] colliders = Physics.OverlapBox(bocCenter, new Vector3(1, 1, 1), transform.rotation);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (alreadyCollidedWith.Contains(collider)) { return; }
                alreadyCollidedWith.Add(collider);
                DealDamage(collider.gameObject);
            }
            //if (collider.gameObject.CompareTag("Player"))
            //{
            //    if (alreadyCollidedWith.Contains(collider)) { return; }
            //    alreadyCollidedWith.Add(collider);
            //    DealDamage(collider.gameObject);
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //適用於角色身上有武器也有collider，可套用在丘丘人
        //if(collision == myCollider) { return; }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (alreadyCollidedWith.Contains(other)) { return; }
            alreadyCollidedWith.Add(other);

            DealDamage(other.gameObject);
        }

        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            //forceReceiver.AddForce(direction * knockback);
        }
    }

    private void WeaponMove()
    {
        if (this.name == "IceArrow(Clone)" || this.name == "Arrow(Clone)")
        {
            currentTime += Time.deltaTime;
            if (currentTime > lifeTime)
            {
                this.gameObject.SetActive(false);
                currentTime = 0;
            }
            PhysicsCollide();

            transform.position += transform.forward * 20 * Time.deltaTime;
        }
        else if (this.name == "IceArrow")
        {

            Vector3 bocCenter = transform.position - transform.forward * 0.5f;
            Debug.DrawLine(transform.position, bocCenter);
            Collider[] colliders = Physics.OverlapBox(bocCenter, new Vector3(1, 1, 1), transform.rotation);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    if (alreadyCollidedWith.Contains(collider)) { return; }
                    alreadyCollidedWith.Add(collider);
                    DealDamage(collider.gameObject);
                    gameObject.SetActive(false);
                }
            }
        }
        else if (this.name == "爪擊_R" || this.name == "爪擊_L")
        {
            PhysicsCollide();
        }
        //else if (this.name == "CFXR Fireball(Clone)")
        //{
        //    //Life Time
        //    currentTime += Time.deltaTime;
        //    if (currentTime > lifeTime)
        //    {
        //        this.gameObject.SetActive(false);
        //        currentTime = 0;
        //    }

        //    //Fly Toward Player
        //    Vector3 vFOr = transform.forward;


        //    vFOr = Vector3.Lerp(vFOr, vToPlayer, 0.1f);

        //    //Make ball Drop
        //    vToPlayer.y -= Time.deltaTime;
        //    vToPlayer = vToPlayer + vFOr;
        //    vToPlayer.Normalize();
        //    transform.forward = vToPlayer;
        //    transform.position = transform.position + vToPlayer * Time.deltaTime * ballSpeed;

        //    //Very Close to Player
        //    if (vToPlayer.sqrMagnitude < 0.05f)
        //    {
        //        Debug.Log("FireBallClose");
        //        PhysicsCollide();
        //    }

        //    //Deal Player Damage，PhysicsCollide有寫了
        //}
        
    }

    public void DealDamage(GameObject Enemy)
    {
        // Enemy Health Decrease
        if (Enemy.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(this.damage);
        }
    }
    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
    }




}
