using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent1 : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    public GameObject iceArrow;
    private Transform arrowStart;
    public ParticleSystem GenyuSkill;
    public ParticleSystem GenyuSkill_01;
    public ParticleSystem GenyuSkill_02;
    public ParticleSystem GenyuSkill_03;
    public GameObject skillBall;


    private void Start()
    {
        arrowStart = GameObject.Find("bowStart").transform;
        skillBall.SetActive(false);
    }
    void Shoot()
    {
        //iceArrow.transform.position = arrowStart.position;
        //iceArrow.SetActive(true);
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
        skillBall.SetActive(true);
        GenyuSkill.Play();
        GenyuSkill_01.Play();
        GenyuSkill_02.Play();
        GenyuSkill_03.Play();
    }

    public void ActivateEffect()
    {

    }
}
