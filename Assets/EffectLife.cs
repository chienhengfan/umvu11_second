using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLife : MonoBehaviour
{
    public float lifetime = 8.0f; // 物体的生存时间，单位秒
    public float fadeDuration = 2.0f; // 渐隐持续时间，单位秒
    private float timer = 0.0f;
    public List<Renderer> objectRenderers;

    void Start()
    {
        //objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // 计时器增加
        timer += Time.deltaTime;

        // 如果计时器超过生存时间，销毁物体
        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }

        // 如果计时器小于渐隐持续时间，逐渐减小透明度
        if (timer < fadeDuration)
        {
            float alpha = 1.0f - (timer / fadeDuration);
            
            foreach(Renderer rd in objectRenderers)
            {
                Color objectColor = rd.material.color;
                objectColor.a = alpha;
                rd.material.color = objectColor;
            }
            
        }
    }
}