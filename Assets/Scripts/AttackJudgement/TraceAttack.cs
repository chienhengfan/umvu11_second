using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        float fDis = vToP.magnitude;
        vToP.Normalize();
        float rightForward = Vector3.Dot(transform.right, vToP);
        float angle = Vector3.Angle(vToP, vFOr);
        if (angle > 60f)
        {
            angle = 0f;
        }
        if (angle > 30f)
        {
            angle = 30f;
        }
        if (rightForward < 0f)
        {
            angle = -angle;
        }
        Quaternion rotate = Quaternion.AngleAxis(angle, Vector3.up);
        vFOr = rotate * vFOr;
        transform.forward = Vector3.Lerp(transform.forward, vFOr, 0.1f);
        Debug.Log("fDis: " + fDis);
        if (fDis < 0.25f)
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

        transform.position = transform.position + transform.forward * Time.deltaTime * ballSpeed;
    }

}