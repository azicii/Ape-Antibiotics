using System;
using System.Collections;
using System.Collections.Generic;
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
        inputReader.CombatOneStartedEvent += PerformFirst;
        inputReader.CombatOneCancelledEvent += CleanupFirst;

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
            if (currentAbilities[i].abilityState == AbilityState.Active)
            {
                currentAbilities[i].abilityActiveTimer -= Time.deltaTime;
                if (currentAbilities[i].abilityActiveTimer <= 0f)
                {
                    currentAbilities[i].abilityState = AbilityState.Cooldown;
                    currentAbilities[i].abilityCooldownTimer = currentAbilities[i].ability.CooldownTime;
                }
            }

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

    // There is room for improvement with this implementation, might change inputReader events to use an int for an index to prevent from rewriting code [Tegomlee].

    private void PerformFirst()
    {
        if (currentAbilities[0].abilityState == AbilityState.Ready)
        {
            currentAbilities[0].ability.Perform(gameObject);
            currentAbilities[0].abilityState = AbilityState.Active;
            currentAbilities[0].abilityActiveTimer = currentAbilities[0].ability.ActiveTime;
        }
    }

    private void CleanupFirst()
    {
        if (currentAbilities[0].abilityState == AbilityState.Active)
        {
            currentAbilities[0].ability.Perform(gameObject);
            currentAbilities[0].abilityState = AbilityState.Active;
            currentAbilities[0].abilityActiveTimer = currentAbilities[0].ability.ActiveTime;
        }
    }
}
