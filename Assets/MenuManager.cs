using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuUI;
    private bool isPaused = false;
    public GameObject cameraController; // 引用相机控制脚本所在的物体
    private void Start()
    {
        menuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleMenu();
        }

        // 检查是否要暂停游戏时间
        if (isPaused)
        {
            Time.timeScale = 0f; // 游戏时间暂停
            Cursor.lockState = CursorLockMode.None; // 解锁鼠标
            Cursor.visible = true; // 显示鼠标
        }
        else
        {
            Time.timeScale = 1f; // 游戏时间正常
            Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标到屏幕中心
            Cursor.visible = false; // 隐藏鼠标
        }
    }

    void ToggleMenu()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(!menuUI.activeSelf); // 切换菜单UI的激活状态
            isPaused = !isPaused; // 切换暂停状态

            // 切换相机控制脚本的激活状态
            if (cameraController != null)
            {
                cameraController.SetActive(!isPaused);
            }
        }
    }
}