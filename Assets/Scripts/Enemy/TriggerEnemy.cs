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
    public List<GameObject> enemiesList = new List<GameObject>();
    private bool stepInTrigger = false;
    public GameObject bossHp;



    //private void Awake()
    //{
    //    mobPoolManager = MobPoolManager.Instance;
    //}

    void Start()
    {
        //enemy.SetActive(false);
        airWall.SetActive(false);
        mobDic = new Dictionary<Transform,MobGroup>();
        sprawnPoints = gameObject.GetComponentsInChildren<Transform>();
        
        foreach(GameObject go in enemiesList)
        {
            if (go.CompareTag(enemytag))
            {
                go.SetActive(false);
            }
        }
    }

    private void Update()
    {

        if (stepInTrigger)
        {
            if (enemiesList.Count > 0)
            {
                foreach (GameObject go in enemiesList)
                {
                    go.SetActive(true);
                    if (go.TryGetComponent<EnemyAnimationEvent>(out EnemyAnimationEvent enemyEvent))
                    {
                        //Play Spawn effect
                        enemyEvent.deadEffect.Play();
                    }
                    if(go.GetComponent<BossEvent>() != null)
                    {
                        bossHp.SetActive(true);
                    }

                }
                StartCoroutine("WaitForWallClose", 2);
            }
        }

        bool enemyAlive = EnemyAlive();
        if (!enemyAlive)
        {
            if(bossHp != null)
            {
                bossHp.SetActive(false);
            }
            airWall.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private bool EnemyAlive()
    {
        if (enemiesList.Count > 0)
        {
            foreach (GameObject go in enemiesList)
            {
                if (go.TryGetComponent<Health>(out Health health))
                {
                    if (health.health > 0)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    IEnumerator WaitForWallClose(int sec)
    {
        yield return new WaitForSeconds(sec);
        airWall.SetActive(true);
        stepInTrigger = false;
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
            //airWall.SetActive(true);
            //enemy.SetActive(true);
            stepInTrigger = true;
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
