using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    public Text hpText;
    private Health Health;

    private void Start()
    {
        // 获取角色的Health脚本
        Health = GetComponent<Health>();

        if (Health == null)
        {
            Debug.LogError("未找到Health组件。");
        }
    }

    private void Update()
    {
        // 更新UI显示为当前HP值
        if (hpText != null && Health != null)
        {
            // 使用角色的生命值字段（假设为currentHealth）
            float currentHP = Health.GetCurrentHealth(Health);
            hpText.text = "HP: " + currentHP.ToString();
        }
    }
}
