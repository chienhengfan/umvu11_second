using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class OpenChest : MonoBehaviour
{
    public GameObject interactionUI; // 可以按"E"的UI元素
    private bool isPlayerNearby = false;
    private bool isBoxOpen = false;

    void Update()
    {
        // 如果玩家靠近物体并按下"E"键，触发事件
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isBoxOpen)
        {
            PerformInteraction();
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
    void PerformInteraction()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }

        if (!isBoxOpen) // 检查宝箱是否已经打开
        {
            Animator boxAnimator = GetComponent<Animator>();

            // 检查Animator组件是否存在
            if (boxAnimator != null)
            {
                // 设置Animator的触发器，以触发开箱子的动画
                boxAnimator.SetTrigger("IsOpen");
                Debug.Log("Player interacted with the object.");
            }
            isBoxOpen = true; // 将宝箱标记为已打开
        }
    }
}
