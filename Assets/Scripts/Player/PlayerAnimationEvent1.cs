using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent1 : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    public GameObject iceArrow;
    private Transform arrowStart;
    [field: SerializeField] public ParticleSystem GenyuSkill { get; private set; }

    private void Start()
    {
        arrowStart = GameObject.Find("bowStart").transform;
    }
    void Shoot()
    {
        Instantiate(iceArrow, arrowStart.position, transform.rotation);
        iceArrow.transform.forward = transform.forward;
    }

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }

    public void UltimateSkill()
    {
        GenyuSkill.Play();
    }
}
