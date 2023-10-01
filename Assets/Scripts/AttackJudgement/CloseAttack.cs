using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : MonoBehaviour
{
    public WeaponDamage weapon;
    [SerializeField] private string targetTag;
    [SerializeField] private float sectorAngle = 60f;
    [SerializeField] private float sectorRadius = 30f;
    [SerializeField] private int attackDamage = 15;
    private GameObject target;


    void Start()
    {

    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        JudgeAttack();
    }

    public bool IsInRange(float sectorAngle, float sectorRadius, GameObject attacker, GameObject target)
    {

        Vector3 direction = target.transform.position - attacker.transform.position;

        float dot = Vector3.Dot(direction.normalized, transform.forward);

        float offsetAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return offsetAngle < sectorAngle * .5f && direction.magnitude < sectorRadius;
    }

    public void JudgeAttack()
    {
        if (IsInRange(sectorAngle, sectorRadius, gameObject, target))
        {
            weapon.SetAttack(attackDamage);
            weapon.DealDamage(target);

        }
    }
}
