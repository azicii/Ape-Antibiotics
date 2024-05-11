using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public GameObject shieldInCanvas;
    private ShieldHealth shield;
    public float shieldHealth;

    public bool cracked => shieldHealth <= 0;
    public bool increasedKnockback;

    [Header("Keybinds")]
    [SerializeField] KeyCode inflictDamageKey = KeyCode.T;

    void Start()
    {
        shield = shieldInCanvas.GetComponentInChildren<ShieldHealth>();
        Knock_back.Instance.Test();
    }

    void Update()
    {
        // Update shield health
        shield.shieldHealth = shieldHealth;

        SelfInflictDamage();
    }


    public void TakeDamage(float damage)
    {
        if (!cracked)
        {
            shieldHealth -= damage;
        }

        if (increasedKnockback)
        {

        }

        if (cracked && !increasedKnockback)
        {
            increasedKnockback = true;
            Debug.Log("Shield Cracked"); // TODO: sound/visual given to player to indicate they will recieve greater knockback now
        }
    }

    private void SelfInflictDamage()
    {
        if (Input.GetKeyDown(inflictDamageKey))
        {
            TakeDamage(10f);
        }
    }
}
