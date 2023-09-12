using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcHP : MonoBehaviour
{
    private Health health;
    public float NpcMaxHp;
    public float NpcHp;

    // Reference to the UI element that displays HP
    public RectTransform hpUI;

    void Start()
    {
        health = GetComponent<Health>();
        NpcMaxHp = health.maxHealth;
        NpcHp = health.currentHealth;
    }

    void Update()
    {
        // Update HP values
        NpcMaxHp = health.maxHealth;
        NpcHp = health.currentHealth;

        // Update the HP UI position based on HP percentage
        float hpPercentage = NpcHp / NpcMaxHp;
        float newXPosition = -272 + 272 * hpPercentage;
        hpUI.localPosition = new Vector3(newXPosition, hpUI.localPosition.y, hpUI.localPosition.z);
    }
}