using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPooler : MonoBehaviour
{
    // List 用来保存对象的引用，实际上的Pool
    public List<GameObject> pooledText;
    // 用来获得上一步创建好的UI Text，这一步非必须可通过其他方法得到。
    public GameObject textToPool;
    // 设置Pool的大小，根据一个对象激活的时间和激活频率来设定
    public int amountToPool;

    void Start()
    {
        pooledText = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            // 在场景中创建对象；
            GameObject obj = (GameObject)Instantiate(textToPool);
            // 将对象放在上一步提到的那个Cavas中，伤害数值也可以时刻面向Camera
            // 这里的false，是让对象继续使用局部坐标。如果继续使用世界坐标，上一步设置的位置会被改变，结果就是看不到。
            obj.transform.SetParent(gameObject.transform, false);
            // 创建后，默认不激活。
            obj.SetActive(false);
            // 将对象引用添加到List中。
            pooledText.Add(obj);

        }
    }

    // 提供一个获取方法
    public GameObject GetPooledText()
    {
        for (int i = 0; i < pooledText.Count; i++)
        {
            // 只会返回未在场景里激活的对象
            if (!pooledText[i].activeInHierarchy)
            {
                return pooledText[i];
            }
        }
        return null;
    }

}
