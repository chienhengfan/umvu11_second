using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventTest01 : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponLogic;

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
