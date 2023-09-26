using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponLogic;
    public CloseAttack closeAttack;

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
        closeAttack.JudgeAttack();
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
}
