using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // 这个变量可以在Inspector中设置，用于标记存档点是否是玩家的复活点
    public bool isRespawnPoint = false;

    // 当玩家到达存档点时调用的方法
    public void ActivateSavePoint()
    {
        if (isRespawnPoint)
        {
            // 在这里触发玩家复活逻辑，例如重置位置、生命值等
            // 还可以保存游戏状态，以便在玩家死亡后恢复
        }
    }
}
