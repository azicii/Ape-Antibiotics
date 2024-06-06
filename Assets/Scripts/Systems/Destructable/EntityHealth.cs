using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This entire class serves no purpose other then to demonstrate how a shattering object looks like [Tegomlee].
 * However, this class can be the entry point to interact with the particle manager should it suffice [Tegomlee].
 */
public class EntityHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float entityHealth;

    public float CurrentHealth => entityHealth;

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage Taken");

        entityHealth -= damage;

        if (entityHealth <= 0)
        {
            ParticleManager.Instance.ActivateParticles(gameObject);
        }
    }
}
