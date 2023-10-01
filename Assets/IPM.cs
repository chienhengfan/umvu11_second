using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class IPM : MonoBehaviour
{
    public Image messageImage;
    public List<Sprite> messageSprites; // 存储不同消息的Sprite
    public float messageDuration = 2.0f; // 提示消息持续时间
    public int maxMessages = 5; // 最大消息数量

    private Queue<int> messageQueue = new Queue<int>();
    private Coroutine displayCoroutine;

    void Start()
    {
        messageImage.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 模拟获取道具
            AddMessage(0); // 0 表示第一条消息，你可以根据需要更改消息索引
        }
    }

    public void AddMessage(int messageIndex)
    {
        messageQueue.Enqueue(messageIndex);

        if (messageQueue.Count > maxMessages)
        {
            messageQueue.Dequeue();
        }

        if (displayCoroutine == null)
        {
            displayCoroutine = StartCoroutine(DisplayMessages());
        }
    }

    IEnumerator DisplayMessages()
    {
        while (messageQueue.Count > 0)
        {
            int messageIndex = messageQueue.Dequeue();

            if (messageIndex >= 0 && messageIndex < messageSprites.Count)
            {
                messageImage.sprite = messageSprites[messageIndex];
                messageImage.enabled = true;
            }

            yield return new WaitForSeconds(messageDuration);
            messageImage.enabled = false;

            yield return new WaitForSeconds(0.1f); // 等待一小段时间，以避免消息显示得太快
        }

        displayCoroutine = null;
    }
}
