using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/*
 * This component requires exactly 4 abilities.
 * 2 are combat and 2 are utility.
 * Will not work if there isnt exactly 4.
 */
public class AbilityManager : MonoBehaviour
{
    // Definitions
    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    [Serializable]
    private class Ability
    {
        public AbilityBase ability;

        [NonSerialized]
        public AbilityState abilityState;

        [NonSerialized]
        public float abilityActiveTimer;

        [NonSerialized]
        public float abilityCooldownTimer;
    }

    //----------

    [Header("Core Attributes")]

    [SerializeField] InputReader inputReader;

    [SerializeField] Ability[] currentAbilities = new Ability[4];

    [SerializeField] Transform attackPoint;

    //----------

    private void Awake()
    {
        // Check if the right amount of abilities is present
        if (currentAbilities.Length != 4)
        {
            Debug.LogError("The wrong amount of abilites is present.");
            return;
        }

        // Subscribe methods to input reader events
        inputReader.AbilityStartedEvent += PerformFirst;
        inputReader.AbilityCancelledEvent += CleanupFirst;

        // Iterate over abilities and set the parameters
        for (int i = 0; i < currentAbilities.Length; i++)
        {
            currentAbilities[i].abilityState = AbilityState.Ready;
            currentAbilities[i].abilityActiveTimer = 0f;
            currentAbilities[i].abilityCooldownTimer = 0f;
        }
    }

    private void Update()
    {
        // Manage the timers for the current abilities array
        for (int i = 0; i < currentAbilities.Length; i++)
        {
            // Timer for any ability that is active
            if (currentAbilities[i].abilityState == AbilityState.Active)
            {
                currentAbilities[i].abilityActiveTimer -= Time.deltaTime;
                if (currentAbilities[i].abilityActiveTimer <= 0f)
                {
                    currentAbilities[i].abilityState = AbilityState.Cooldown;
                    currentAbilities[i].abilityCooldownTimer = currentAbilities[i].ability.CooldownTime;
                }
            }

            // Timer for any ability that is in cool down
            else if (currentAbilities[i].abilityState == AbilityState.Cooldown)
            {
                currentAbilities[i].abilityCooldownTimer -= Time.deltaTime;
                if (currentAbilities[i].abilityCooldownTimer <= 0f)
                {
                    currentAbilities[i].abilityState = AbilityState.Ready;
                }
            }
        }
    }

    //----------

    private void PerformFirst(int abilityIndex)
    {
        if (currentAbilities[abilityIndex].abilityState == AbilityState.Ready)
        {
            currentAbilities[abilityIndex].ability.Perform(gameObject);
            currentAbilities[abilityIndex].abilityState = AbilityState.Active;
            currentAbilities[abilityIndex].abilityActiveTimer = currentAbilities[abilityIndex].ability.ActiveTime;
        }
    }

    private void CleanupFirst(int abilityIndex)
    {
        if (currentAbilities[abilityIndex].abilityState == AbilityState.Active)
        {
            // Get the time of the abilityActiveTimer
            float currentAblilityActiveTime = currentAbilities[abilityIndex].abilityActiveTimer;

            currentAbilities[abilityIndex].ability.Cleanup(gameObject, currentAblilityActiveTime);
            currentAbilities[abilityIndex].abilityState = AbilityState.Active;
            currentAbilities[abilityIndex].abilityActiveTimer = currentAbilities[abilityIndex].ability.ActiveTime;
        }
    }

    //----------

    public Vector3 GetAttackPosition()
    {
        return attackPoint.position;
    }
}
