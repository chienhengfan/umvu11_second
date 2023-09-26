using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipS : MonoBehaviour
{
    public GameObject TextLog; // 可以按"E"的UI元素
    public GameObject Text;
    private bool isPlayerNearby = false;

    void Update()
    {
        // 如果玩家靠近物体并按下"E"键，触发事件
        if (isPlayerNearby)
        {
            //PerformInteraction();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (TextLog != null)
            {
                TextLog.SetActive(true);
            }
            if (Text != null)
            {
                Text.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (TextLog != null)
            {
                TextLog.SetActive(false);
            }
            if (Text != null)
            {
                Text.SetActive(false);
            }
        }
    }
}
