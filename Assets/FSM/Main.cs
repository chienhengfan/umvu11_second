using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main m_Instance;
    public Object enemyObject;
    private GameObject m_Player;

    private GameObject[] _enemies;
    private List<Obstacle> m_Obstacles;
    



    private void Awake()
    {
        m_Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");

        m_Obstacles = new List<Obstacle>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Obstacle");
        if (gos != null || gos.Length > 0)
        {
            foreach (GameObject go in gos)
            {
                m_Obstacles.Add(go.GetComponent<Obstacle>());
            }
        }
        //GenerateEnemies(5);
        //Debug.Log("enemy done");
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    private void Update()
    {
        ThirdPersonController playerT = m_Player.GetComponent<ThirdPersonController>();
        float pHp = playerT.playerHP;
        if(pHp <= 0)
        {
            foreach(var go in _enemies)
            {
                Animator an = go.GetComponent<Animator>();
                an.enabled = false;
            }
        }
    }

    private void GenerateEnemies(int num)
    {

        if (enemyObject == null)
        {
            return;
        }
        _enemies = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            GameObject go = GameObject.Instantiate(enemyObject) as GameObject;
            Vector3 vdir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            if (vdir.magnitude < 0.001f)
            {
                vdir.x = 1.0f;
            }
            vdir.Normalize();
            go.transform.position = vdir * Random.Range(10.0f, 20.0f);
            _enemies[i] = go;
        }

    }

    public GameObject GetPlayer()
    {
        return m_Player;
    }

    public List<Obstacle> GetObstacles()
    {
        return m_Obstacles;
    }
}
