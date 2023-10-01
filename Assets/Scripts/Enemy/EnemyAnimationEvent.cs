using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponLogic;
    [Tooltip("ˋ專屬於crossbow丘丘人子物件的bowFront")]
    [SerializeField]private Transform bowShootStart;
    [SerializeField]private GameObject ChuChuArow;
    private GameObject player;
    private int weaponNum = 0;

    //public CloseAttack closeAttack;
    [SerializeField] private float sectorAngle = 60f;
    [SerializeField] private float sectorRadius = 15f;
    [SerializeField] private int crawlAttackDamage = 5;
    public WeaponDamage weaponDamage;
    [SerializeField] private AudioClipsPlayer audioPlayer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

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

    public void Shoot()
    {
        GameObject shootGo = Instantiate(ChuChuArow, transform.position, Quaternion.identity);
        shootGo.transform.position = bowShootStart.position;
        Vector3 toP = player.transform.position - bowShootStart.position + player.transform.up* 0.5f;
        shootGo.transform.forward = toP;
    }

    public bool IsInRange(float sectorAngle, float sectorRadius, GameObject attacker, GameObject target)
    {

        Vector3 direction = target.transform.position - attacker.transform.position;

        float dot = Vector3.Dot(direction.normalized, transform.forward);

        float offsetAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return offsetAngle < sectorAngle * .5f && direction.magnitude < sectorRadius;
    }

    public void IntroAudio()
    {
        if(audioPlayer != null)
        {
            Debug.Log("IntroAudio");
            audioPlayer.PlayAudioClip(0);
        }
    }

    public void AttackAudio()
    {
        if (audioPlayer != null)
        {
            Debug.Log("AttackAudio");
            audioPlayer.PlayAudioClip(1);
        }
    }

    public void DamagedAudio()
    {
        if (audioPlayer != null)
        {
            Debug.Log("DamagedAudio");
            audioPlayer.PlayAudioClip(2);
        }
    }

    public void DeadAudio()
    {
        if (audioPlayer != null)
        {
            Debug.Log("DeadAudio");
            audioPlayer.PlayAudioClip(3);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sectorRadius);
    }
}
