using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject interactionUI; // 可以按"E"的UI元素
    private bool isPlayerNearby = false;
    public IPM IPM;
    void Update()
    {
        // 显示或隐藏UI提示
        // 如果玩家靠近物体并按下"E"键，触发事件
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PerformInteraction(); // 触发事件的方法
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactionUI != null)
            {
                interactionUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionUI != null)
            {
                interactionUI.SetActive(false);
            }
        }
    }

    // 触发事件的方法
    public void PerformInteraction()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
        // 通知ItemPickupManager处理拾取
        if (IPM != null)
        {
            //IPM.AddMessage(messageIndex); // 这里你需要提供消息索引
        }
        Destroy(gameObject);
        Debug.Log("Player interacted with the object.");
    }
}
