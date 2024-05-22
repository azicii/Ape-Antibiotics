using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Punch", menuName = "Abilities/Punch")]
public class AbPunch : AbilityBase
{
    [Header("Specific Attributes")]

    [Tooltip("The minimum amount of damage.")]
    [SerializeField] float minDamage = 10f;

    [Tooltip("The maximum amount of damage.")]
    [SerializeField] float maxDamage = 30f;

    [Tooltip("The layers affected by the attack.")]
    [SerializeField] LayerMask affectedLayers;

    [Tooltip("The amount of knockback applied to the enemy.")]
    [SerializeField] float baseKnockbackForce;

    public override void Perform(GameObject parent)
    {
        // Perform Charging animation
        // Add the line after it is in the game [Tegomlee].

        // Remove the return when the animation is in the game [Tegomlee].
        return;
    }

    public override void Cleanup(GameObject parent, float timeSinceAbilityStarted)
    {
        // Perform the attack animation
        Animator animator = parent.GetComponent<Animator>();
        animator.SetTrigger("Punch");

        // Calculate the time difference between the active time and time since attack
        float timeDifference = activeTime - timeSinceAbilityStarted;
        float timePercantage = timeSinceAbilityStarted / activeTime;

        // Calculate the final damage value
        float finalDamageValue = Mathf.Lerp(minDamage, maxDamage, timePercantage);

        // Calculate the final knocknback value
        float finalKnockbackValue = baseKnockbackForce * timeDifference;

        // Detect the objects in range of attack
        Vector3 attackPosition = parent.GetComponent<AbilityManager>().GetAttackPosition();
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, 0.5f, affectedLayers);

        // Affect the detected objects
        foreach (Collider hitCollider in hitColliders)
        {
            // Damage the objects
            if (hitCollider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(finalDamageValue);
            }

            // Knockback the objects
            if (hitCollider.TryGetComponent(out IKnockable knockable))
            {
                // Prepare the object for knockback
                knockable.PrepareForKnockback();

                // Apply the knockback
                Knockback.Instance.PerformKnockbackBetweenTwoPoints(hitCollider, attackPosition, finalKnockbackValue);
            }
        }
    }
}
