using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay = 2.0f; // 设置销毁的延迟时间，单位是秒

    void Start()
    {
        // 在指定的延迟时间后调用DestroyObject方法
        Invoke("DestroyObject", delay);
    }

    void DestroyObject()
    {
        // 销毁该物体
        Destroy(gameObject);
    }

}
