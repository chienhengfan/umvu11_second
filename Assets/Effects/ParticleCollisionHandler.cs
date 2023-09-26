using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    public ParticleSystem explosionPrefab; // 爆炸特效的预制体

    private ParticleSystem explosionInstance; // 实际爆炸特效的实例
    public int damageAmount = 10; // 伤害值
    private void Start()
    {
        // 在开始时实例化爆炸特效，但不激活
        explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionInstance.gameObject.SetActive(false);
    }

    private void OnParticleCollision(GameObject other)
    {
        {
            // 检查碰撞的对象是否是敌人
            //Health enemy = other.GetComponent<Enemy>();

            //if (enemy != null)
            //{
            //    // 对敌人应用伤害
            //    enemy.TakeDamage(damageAmount);
            //}
        }
        //Debug.Log("+++++++++++++++");
        //// 当粒子碰撞时，激活爆炸特效
        //if (explosionInstance != null)
        //{
        //    explosionInstance.transform.position = transform.position; // 设置特效位置
        //    explosionInstance.gameObject.SetActive(true);
        //    Debug.Log("+++++++++++++++");
        //}
        //Debug.Log("+++++++++++++0000000000000000000");
        //// 禁用当前游戏对象
        //gameObject.SetActive(false);
    }




}
