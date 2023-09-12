using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    public int damageAmount = 10; // 伤害值

    void OnTriggerEnter(Collider collision)
    {
        // 检查碰撞对象是否是目标（根据标签、层或其他条件进行判断）
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 对目标造成伤害
            DealDamage(collision.gameObject);
        }
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Arrow hit an enemy!");
            // 执行伤害逻辑
        }
        // 在碰撞后，销毁弓箭或进行其他处理
        //Destroy(gameObject);
    }
    void DealDamage(GameObject Enemy)
    {
        // 在这里编写对目标造成伤害的逻辑
        // 可能需要访问目标的生命值脚本，并减少其生命值

        // 示例：获取目标的生命值脚本
        Health health = Enemy.GetComponent<Health>();

        if (health != null)
        {
            // 减少目标生命值
            health.TakeDamage(damageAmount);
        }
    }

}
