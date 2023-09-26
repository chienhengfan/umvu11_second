using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public WeaponDamage weapon;
    [SerializeField] private string targetTag;
    private GameObject target;

    private float lifeTime = 2.0f;
    private float currentTime = 0;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float judgementHitDistance = 0.1f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            gameObject.SetActive(false);
            currentTime = 0;
        }


        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float distanceToTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if(distanceToTarget <= judgementHitDistance)
        {
            weapon.SetAttack(attackDamage);
            weapon.DealDamage(target);
            gameObject.SetActive(false);
        }
    }


}
