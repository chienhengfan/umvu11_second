using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    private Transform[] sprawnPoints;

    private Dictionary<Transform, MobGroup> mobDic;
    public GameObject airWall;
    public GameObject enemy;
    public string enemytag = "Enemy";
    //public MobPoolManager mobPoolManager;




    //private void Awake()
    //{
    //    mobPoolManager = MobPoolManager.Instance;
    //}

    void Start()
    {
        enemy.SetActive(false);
        airWall.SetActive(false);
        mobDic = new Dictionary<Transform,MobGroup>();
        sprawnPoints = gameObject.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if(enemy.GetComponentInChildren<EnemyStateMachine>() == null)
        {
            airWall.SetActive(false);
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player"))
    //    {
    //        airWall.SetActive(true);
    //        foreach(KeyValuePair<Transform, MobGroup> info in mobDic)
    //        {
    //            enemySprawlEffect.Play();
    //            mobPoolManager.MobGenerate(info.Value, info.Key);
    //        }

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            airWall.SetActive(true);
            enemy.SetActive(true);

        }
    }

    bool CheckChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
