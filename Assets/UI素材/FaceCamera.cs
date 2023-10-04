using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera; // 参考主摄像机

    private void Start()
    {
        // 获取场景中的主摄像机
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("没有找到主摄像机，请确保场景中有一个激活的摄像机。");
        }
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            // 让Canvas的正面朝向摄像机位置
            transform.LookAt(mainCamera.transform);
        }
    }
}
