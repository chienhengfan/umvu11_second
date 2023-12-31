using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceAttack : MonoBehaviour
{

    public float radius = 1.0f;

    public WeaponDamage weapon;
    public ParticleSystem explosion;

    private float ballSpeed = 5.0f;

    [SerializeField] private string targetTag;
    private GameObject target;
    private float ballDropTime = 0.0f;
    private float fLifeTime = 2.0f;

    [SerializeField] private int attackDamage = 15;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    void Update()
    {
        Vector3 vFOr = transform.forward;
        Vector3 vPlayerChasePoint = target.transform.position + target.transform.up * 1.0f;
        Vector3 vToP = vPlayerChasePoint - transform.position;
        vFOr = Vector3.Lerp(vFOr, vToP, 0.1f);


        if (vToP.magnitude < 0.1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            ballDropTime = 0.0f;
            weapon.SetAttack(attackDamage);
            weapon.DealDamage(target);

            gameObject.SetActive(false);
        }
        if (ballDropTime >= fLifeTime)
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
        transform.position = transform.position + vToP * Time.deltaTime * ballSpeed;
    }

}