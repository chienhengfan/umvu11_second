using MagicaCloth2;
using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [HideInInspector]
    public int damageAmount = 10;
    [HideInInspector]
    public float maxHealth; // 最大生命值
    //[HideInInspector]
    public float currentHealth; // 当前生命值
    public RectTransform hpUI;
    public GameObject hpBarUI;
    private bool isDead = false; // 是否死亡
    [HideInInspector]
    public SimpleFSM2 fSM2;
    public GameObject hitNumberPrefab;
    [SerializeField] TextPooler TextPool;

    public BossFSM bossFSM;

    void Start()
    {
        if (this.gameObject.name == "LaSignora_Harbinger")
        {
            maxHealth = GetComponent<BossFSM>().m_Data.m_fHp;
        }
        else
        {
            maxHealth = GetComponent<SimpleFSM2>().m_Data.m_fHp;
        }
        
        currentHealth = maxHealth; // 初始化当前生命值为最大生命值
        fSM2 = GetComponent<SimpleFSM2>();
        bossFSM = GetComponent<BossFSM>();
    }




    private void Update()
    {
        float hpPercentage = (float)currentHealth / maxHealth;
        float newXPosition = -272 + 272 * hpPercentage;
        hpUI.localPosition = new Vector3(newXPosition, hpUI.localPosition.y, hpUI.localPosition.z);
    }

    // 受伤函数，传递受伤值
    public void TakeDamage(int damage)
    {
        if (isDead) return; // 如果已经死亡，不再受伤

        currentHealth -= damage; // 减去受伤值


        if (this.gameObject.name == "LaSignora_Harbinger")
        {
            bossFSM.m_Am.SetTrigger("IsDamaged");
        }
        else
        {
            fSM2.m_Am.SetTrigger("IsDamaged");
        }

        
        // 更新UI、播放受伤动画或其他受伤反馈
        if (currentHealth <= 0)
        {
            Die(); // 如果生命值小于等于0，触发死亡
        }
        var Arrow = gameObject.transform.GetComponent<ArrowDamage>();
        if (Arrow)
        {
            //damage = damageAmount;
        }
        int damageAmount = 10; // 假设伤害数值是10
        GenerateHitNumber(damageAmount, this.transform.position + Vector3.up * 0.5f);
        GenerateHitNumber(damage, this.transform.position + Vector3.up * 0.5f);
    }

    // 死亡函数
    void Die()
    {
        isDead = true;

        // 触发死亡动画、播放音效、显示游戏结束界面或其他死亡相关操作

        // 可以在这里写额外的死亡逻辑
        Debug.Log("Dead moment");

        if (this.gameObject.name == "LaSignora_Harbinger")
        {
            bossFSM.m_Data.m_fHp = 0;
            bossFSM.m_Am.SetTrigger("IsDead");
        }
        else
        {
            fSM2.m_Data.m_fHp = 0;
            fSM2.m_Am.SetTrigger("IsDead");
        }


        hpBarUI.SetActive(false);
    }

    // 增加生命值的函数
    public void Heal(int amount)
    {
        if (isDead) return; // 如果已经死亡，不再回血

        currentHealth += amount;

        // 更新UI或其他回血反馈

        // 确保不超过最大生命值
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void GenerateHitNumber(int damage, Vector3 pos)
    {
        var numberObject = Instantiate(hitNumberPrefab, pos, Quaternion.identity);
        var textComponent = numberObject.GetComponentInChildren<TMP_Text>();
        Debug.Log("GenerateHitNumber======");
        if (textComponent != null)
        {
            Debug.Log("GenerateHitNumber1================");
            textComponent.text = damage.ToString();
        }
    }

    //public void GenerateHitNumber(int number, Vector3 pos)
    //{
    //    var numberObject = Instantiate(hitNumberPrefab, pos, Quaternion.identity);
    //    numberObject.GetComponentInChildren<TextMesh>().text = number.ToString();
    //if (numberObject != null)
    //{
    //    Debug.Log("number: " + number);
    //    Debug.Log(numberObject.GetComponentInChildren<Text>().text);
    //}

    void PopDamageText(float amount)
    {
        // 从Text对象池中，获取一个未激活的Text
        var textObj = TextPool.GetPooledText();
        // 设定初始位置，如果和Prefab的一致，可以省略，注意是局部坐标
        textObj.transform.localPosition = new Vector3(0, 100f, 0);
        // 设置Text的显示内容
        textObj.GetComponent<Text>().text = "-" + amount;
        // 激活Text
        textObj.SetActive(true);
        // 一定时间后，将该Text重新变为非激活，注意不是Destroy
        StartCoroutine(DelText(textObj));

    }

    IEnumerator DelText(GameObject textObj)
    {
        yield return new WaitForSeconds(2f);
        textObj.SetActive(false);
        textObj.GetComponent<Text>().text = "";
    }
    public static float GetCurrentHealth(Health character)
    {
        return character.currentHealth;
    }

    public void Hit()
    {
        Debug.Log("Hit Sth");
    }

    public void FootR()
    {
        Debug.Log("FootR");
    }
    public void FootL()
    {
        Debug.Log("FootL");
    }
    
}

