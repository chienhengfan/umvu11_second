using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using test;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private readonly int DeadHash = Animator.StringToHash("Dead");
    private Animator anim;

    public int maxHealth = 100;
    public int health;

    [Header("HP 介面控制")]
    [Tooltip("取得 Canvas 最底層血量條Image")]
    public RectTransform hpImage;
    [Tooltip("取得血條物件 ，小怪與王在本身子物件 Canvas ，Player在場景 甘雨Hp")]
    public GameObject hpBarUI;
    [Tooltip("控制血量條平移，玩家460，小怪 Hp number 設定272， Boss Hp number 設定272")]
    public int hpLocalNum;
    

    [Header("傷害跳數字")]
    [Tooltip("跳出傷害數字的面板，取用 TMP_Text 元件改變傷害數字")]
    [SerializeField] GameObject hitNumberPrefab;
    private int mobIndex;

    public event Action OnTakeDamage;
    public event Action OnDie;

    private GameObject bossHP;



    void Start()
    {
        //bossHP = GameObject.Find("BossHp");
        health = maxHealth;

        anim = GetComponent<Animator>();
        if(this.TryGetComponent<EnemyStateMachine>(out EnemyStateMachine esm))
        {
            mobIndex = esm.MobEnumIndex;
        }
    }

    private void Update()
    {
        float currentHealth = (float)health;
        float hpPercentage = (float)currentHealth / maxHealth;
        float newXPosition = -hpLocalNum + hpLocalNum * hpPercentage;
        hpImage.localPosition = new Vector3(newXPosition, hpImage.localPosition.y, hpImage.localPosition.z);
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) { return; }
        health = Mathf.Max(health - damage, 0);
        float healthRatio = (float)health / maxHealth;
        if(mobIndex == EnemyStateMachine.MobGroup.BossLady.GetHashCode())
        {
            if(healthRatio <= 0.5f && healthRatio > 0.3f)
            {
                OnTakeDamage?.Invoke();
            }
        }
        else
        {
            OnTakeDamage?.Invoke();
        }
        GenerateHitNumber(damage, this.transform.position + Vector3.up * 0.5f);

        if (health <= 0)
        {
            OnDie?.Invoke();
            anim.Play(DeadHash);
            
            if (!this.CompareTag("Player"))
            {
                StartCoroutine(DisableObject());
                hpBarUI.SetActive(false);
            }
            //if (bossHP.activeSelf)
            //{
            //    bossHP.SetActive(false);
            //}

        }
    }

    private void GenerateHitNumber(int damage, Vector3 pos)
    {
        var numberObject = Instantiate(hitNumberPrefab, pos, Quaternion.identity);
        var textComponent = numberObject.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = damage.ToString();
        }
    }

    public IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


}
