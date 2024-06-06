using System;
using UnityEngine;

public class PlayerShield : MonoBehaviour, IDamageable
{
    [Tooltip("The maximum value the shield has.")]
    [SerializeField] float maxShieldHealth = 100f;

    [Tooltip("How much time (in seconds) has to pass before the shield begins recharging.")]
    [SerializeField] float timeBeforeRecharge = 3f;

    [Tooltip("How fast the shield recharges (in seconds) to completion.")]
    [SerializeField] float shieldRechargeRate = 0.3f;

    //----------

    // Variables
    private float currentShieldHealth;
    private bool isCracked = false;

    public bool IsCracked
    {
        get { return isCracked; }
    }

    private float timeSinceDamage;

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    // Events
    public static event Action<float, float> ShieldChangedEvent;

    //----------

    [Header("Debugging")]
    [SerializeField] KeyCode inflictDamageKey = KeyCode.T;

    //----------

    private void Awake()
    {
        // Set the current health
        currentShieldHealth = maxShieldHealth;

        // Invoke the event
        ShieldChangedEvent?.Invoke(currentShieldHealth, maxShieldHealth);

        // Set timeSinceDamaged to prevent unnecessary calls
        timeSinceDamage = timeBeforeRecharge;
    }

    private void Update()
    {
        // Debugging
        SelfInflictDamage();

        // Count up the shield recharge timer
        if (timeSinceDamage < timeBeforeRecharge)
        {
            timeSinceDamage += Time.deltaTime;
        }

        // Recharge the shield, if neccessary
        if (timeSinceDamage >= timeBeforeRecharge && currentShieldHealth < maxShieldHealth)
        {
            RechargeShield();
        }
    }

    /*
     * Refactored the Method to use the IDamageable interface, this will make it easier
     * to call this method on anything that can take damage moving forward. [Tegomlee]
     */
    public void TakeDamage(float damage)
    {
        // Set timer to 0
        timeSinceDamage = 0f;
        
        if (isCracked)
        {
            isDead = true;
            Debug.Log("Player is Dead!");
            return;
        }
        
        // Take damage
        currentShieldHealth -= damage;

        // Check for state
        if (currentShieldHealth <= 0f) 
        {
            currentShieldHealth = 0f;
            
            // Debugging 
            Debug.Log("Shield Health - " + currentShieldHealth.ToString());
            
            isCracked = true;
            
            ScoreManager.Instance.LoseCoins();
        }
        else
        {
            isCracked = false;
        }

        // Invoke the event
        ShieldChangedEvent?.Invoke(currentShieldHealth, maxShieldHealth);

        // Debugging
        if (isCracked)
        {
            Debug.Log("Shield Cracked");
        }
    }

    private void RechargeShield()
    {
        // Increase the current shield
        currentShieldHealth += shieldRechargeRate * Time.deltaTime;

        // Clamp the value to the maximum shield value
        currentShieldHealth = Mathf.Clamp(currentShieldHealth, 0, maxShieldHealth);

        // Invoke the event
        ShieldChangedEvent?.Invoke(currentShieldHealth, maxShieldHealth);
    }

    private void SelfInflictDamage()
    {
        if (Input.GetKeyDown(inflictDamageKey))
        {
            TakeDamage(10f);
        }
    }

    public float GetShieldHealth()
    {
        //Debug.Log("Get Shield Health - " + currentShieldHealth.ToString());
        return currentShieldHealth;
    }

    public void SetShieldHealth(float shieldHealth)
    {
        currentShieldHealth = shieldHealth;

        //Debug.Log("Set Shield Health - "+shieldHealth.ToString());
    }
    
}
