using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float enemyMaxHP;
    public float enemyCurrentHp;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyMaxHP = enemy.GetComponent<Health>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCurrentHp = enemy.GetComponent<Health>().health;
        //Debug.Log("current+" + enemyCurrentHp);
        //Debug.Log("max" + enemyMaxHP);

        this.transform.localPosition = new Vector3((-267 + 267*(enemyCurrentHp / enemyMaxHP)), 0.0f, 0.0f);
    }
}
