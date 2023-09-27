using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public WeaponDamage weapon;
    [SerializeField] private string targetTag;
    private List<GameObject > targets;
    private bool isShoot = false;

    private float lifeTime = 2.0f;
    private float currentTime = 0;
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float judgementHitDistance = 0.1f;

    private void Start()
    {
        targets = new List<GameObject>();

        GameObject[] gos  = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject go in gos)
        {
            targets.Add(go);
        }
    }

    private void Update()
    {

        currentTime += Time.deltaTime;
        if (currentTime > lifeTime)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            currentTime = 0;
        }



        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        foreach(GameObject go in targets)
        {
            float distanceToTarget = Vector3.Distance(go.transform.position, gameObject.transform.position);

            if (distanceToTarget <= judgementHitDistance)
            {
                weapon.SetAttack(attackDamage);
                weapon.DealDamage(go);
                Destroy(go);
                gameObject.SetActive(false);
            }
        }
       
    }


}
