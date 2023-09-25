using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    public int health;

    public event Action OnTakeDamage;
    public event Action OnDie;
    [SerializeField] GameObject Deadmenu = null;



    void Start()
    {
        health = maxHealth;
        Deadmenu.SetActive(false);
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) { return; }
        health = Mathf.Max(health - damage, 0);
        Debug.Log(this.name + ": " + health);
        OnTakeDamage?.Invoke();

        if (health <= 0)
        {
            OnDie?.Invoke();
            Deadmenu.SetActive(true);
        }
    }
}
