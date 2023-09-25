using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachineTest01 : StateMachineTest01
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiverTest01 ForceReceiver { get; private set; }
    [field: SerializeField] public List<WeaponDamageTest01> Weapons { get; private set; }
    [field: SerializeField] public HealthTest01 Health { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int MobEnumIndex { get; private set; }

    public GameObject Player { get; private set; }

    public enum MobGroup
    {
        ChuCHu, CHuCHuCrossbow, AbyssMage
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        SwitchState(new EnemyIdleStateTest01(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactStateTest01(this));
    }
    private void HandleDie()
    {
        SwitchState(new EnemyDeadStateTest01(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
