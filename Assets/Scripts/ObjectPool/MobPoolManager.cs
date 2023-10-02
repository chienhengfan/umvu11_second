using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobGroup
{
    ChuCHu, CHuCHuCrossbow, AbyssMage, BossLady
}

public class MobPoolManager : MonoBehaviour
{
    public GameObject chuchu;
    public GameObject CHuCHuCrossbow;
    public GameObject AbyssMage;
    public GameObject BossLady;
    private static MobPoolManager _instance;
    public static MobPoolManager Instance { get { return _instance ?? new MobPoolManager(); } }

    public ObjectPool<EnemyStateMachine> mobPool;


    private MobPoolManager()
    {
        _instance = this;
    }
    private void Awake()
    {
        mobPool = ObjectPool<EnemyStateMachine>.Instance;
    }

    void Start()
    {
        mobPool.Initialize(chuchu);
    }


    public EnemyStateMachine MobGenerate(MobGroup mobGroup, Transform transform)
    {
        EnemyStateMachine enemy = null;
        switch (mobGroup)
        {
            case MobGroup.ChuCHu:
                enemy = mobPool.SpawnObject(chuchu, transform.position, chuchu.transform.rotation);
                break;
            case MobGroup.CHuCHuCrossbow:
                enemy = mobPool.SpawnObject(CHuCHuCrossbow, transform.position, CHuCHuCrossbow.transform.rotation);
                break;
            case MobGroup.AbyssMage:
                enemy = mobPool.SpawnObject(AbyssMage, transform.position, AbyssMage.transform.rotation);
                break;
            case MobGroup.BossLady:
                enemy = mobPool.SpawnObject(BossLady, transform.position, BossLady.transform.rotation);
                break;

        }
        return enemy;
    }


}
