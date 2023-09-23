using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTest01 : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    public event Action OnTakeDamage;


    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) { return; }
        health = Mathf.Max(health - damage, 0);
        Debug.Log(this.name +": "+  health);
        OnTakeDamage?.Invoke();
    }
}
