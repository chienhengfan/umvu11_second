using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponLogic;
    //public CloseAttack closeAttack;
    [SerializeField] private float sectorAngle = 60f;
    [SerializeField] private float sectorRadius = 15f;
    [SerializeField] private int crawlAttackDamage = 5;
    public WeaponDamage weaponDamage;


    public void EnableWeapon()
    {
        foreach (var weapon in weaponLogic)
        {
            weapon.SetActive(true);
        }
    }

    public void DisableWeapon()
    {
        foreach (var weapon in weaponLogic)
        {
            weapon.SetActive(false);
        }
    }

    public void CrawlAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(IsInRange(sectorAngle, sectorRadius, gameObject, player))
        {
            weaponDamage.SetAttack(crawlAttackDamage);
            weaponDamage.DealDamage(player);
        }
    }

    public void Hit()
    {
        //Hit Sth
    }
    public void FootL()
    {
        //
    }
    public void FootR()
    {
        //
    }

    public bool IsInRange(float sectorAngle, float sectorRadius, GameObject attacker, GameObject target)
    {

        Vector3 direction = target.transform.position - attacker.transform.position;

        float dot = Vector3.Dot(direction.normalized, transform.forward);

        float offsetAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return offsetAngle < sectorAngle * .5f && direction.magnitude < sectorRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sectorRadius);
    }
}
