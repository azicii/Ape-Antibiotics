using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    [Header("Core Attributes")]

    [Tooltip("The amount of time (in seconds) an ability is active.")]
    [SerializeField] protected float activeTime;
    /// <summary>
    /// The amount of time (in seconds) an ability is active.
    /// </summary>
    public float ActiveTime { get { return activeTime; } }

    [Tooltip("The amount of time (in seconds) an ability cools down.")]
    [SerializeField] protected float cooldownTime;
    /// <summary>
    /// The amount of time (in seconds) an ability cools down.
    /// </summary>
    public float CooldownTime { get { return cooldownTime; } }

    private enum AbilityType
    {
        Combat,
        Utility
    }

    [Tooltip("The ability's type.")]
    [SerializeField] AbilityType typeOfAbility;

    /// <summary>
    /// Compares the current ability to <c>AbilityType.Combat</c>.
    /// </summary>
    public bool IsCombatAbility { get { return typeOfAbility == AbilityType.Combat; } }

    /// <summary>
    /// Compares the current ability to <c>AbilityType.Utility</c>.
    /// </summary>
    public bool IsUtilityAbility { get { return typeOfAbility == AbilityType.Utility; } }
    
    /// <summary>
    /// This is where all the logic to execute the ability will go. This is an abstract method so all subclasses will need to implement thier own logic.
    /// </summary>
    /// <param name="parent">The player</param>
    public abstract void Perform(GameObject parent);

    /// <summary>
    /// This is where all the logic to continue the ability execution will go. This method is mainly used if the ability require additional functionality beyond <c>Perform()</c>.
    /// </summary>
    /// <param name="parent">The player</param>
    public virtual void Calculate(GameObject parent)
    {
        // The default implementation of Calculate(), Override if necessary [Tegomlee]
        return;
    }

    /// <summary>
    /// This is where all neccesary ability clean up will execute (if necessary).
    /// </summary>
    public virtual void Cleanup()
    {
        // The default implementation of Cleanup(), Override if necessary [Tegomlee]
        return;
    }
}
