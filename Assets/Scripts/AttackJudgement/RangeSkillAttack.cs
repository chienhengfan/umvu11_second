using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSkillAttack : MonoBehaviour
{

    [SerializeField] private int skillDamage = 15;
    public WeaponDamage weaponDamage;
    [SerializeField] private string target = "Enemy";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(target))
        {
            weaponDamage.SetAttack(skillDamage);
            weaponDamage.DealDamage(other.gameObject);
        }


    }
}
