using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BossEvent : MonoBehaviour
{
    public WeaponDamage weaponDamage;
    public ParticleSystem energyBall;
    public Vector3 vMove;

    private Transform shootStart;
    private ParticleSystem[] EnergyBalls = null;
    private int ballNum = 10;
    private int currentBallNum = 0;

    public float AttackLady02Angle = 60f;
    public float dAttackLady02Radius = 15f;
    public int AttackLady02Damage = 35;
    public ParticleSystem AttackLady02Effect;


    public float AttackLady04Angle = 360f;
    public float dAttackLady04Radius = 20f;
    public int AttackLady04Damage = 17;
    public ParticleSystem AttackLady04Effect;

    public ParticleSystem LadySkill01Effect;
    public ParticleSystem deadEffect;


    private void Start()
    {


        EnergyBalls = new ParticleSystem[ballNum];

        for (int i = 0; i < EnergyBalls.Length; i++)
        {
            EnergyBalls[i] = Instantiate(energyBall);
            EnergyBalls[i].gameObject.SetActive(false);
        }
    }



    void EnergyBallControlEvent()
    {
        shootStart = gameObject.transform.Find("ShootStart");
        //Debug.Log("Event Ball");
        currentBallNum = currentBallNum % ballNum;
        EnergyBalls[currentBallNum].gameObject.transform.position = shootStart.position;
        EnergyBalls[currentBallNum].gameObject.transform.forward = shootStart.forward;
        EnergyBalls[currentBallNum].gameObject.SetActive(true);

        currentBallNum++;
    }



    void AttackLady02()
    {
        AttackLady02Effect.gameObject.SetActive(true);
        AttackLady02Effect.Play();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (IsInRange(AttackLady02Angle, dAttackLady02Radius, gameObject, player))
        {
            weaponDamage.SetAttack(AttackLady02Damage);
            weaponDamage.DealDamage(player);
        }
    }

    void AttackLady04()
    {
        AttackLady04Effect.gameObject.SetActive(true);
        AttackLady04Effect.Play();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (IsInRange(AttackLady04Angle, dAttackLady04Radius, gameObject, player))
        {
            weaponDamage.SetAttack(AttackLady04Damage);
            weaponDamage.DealDamage(player);
        }
    }

    void LadySkill01()
    {
        LadySkill01Effect.gameObject.SetActive(true);
        LadySkill01Effect.Play();
    }

    void CloseEffect()
    {
        AttackLady02Effect.gameObject.SetActive(false);
        AttackLady04Effect.gameObject.SetActive(false);
        LadySkill01Effect.gameObject.SetActive(false);
    }


    public bool IsInRange(float sectorAngle, float sectorRadius, GameObject attacker, GameObject target)
    {

        Vector3 direction = target.transform.position - attacker.transform.position;

        float dot = Vector3.Dot(direction.normalized, transform.forward);

        float offsetAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return offsetAngle < sectorAngle * .5f && direction.magnitude < sectorRadius;
    }

    void DeadEffect()
    {
        deadEffect.Play();
    }
}
