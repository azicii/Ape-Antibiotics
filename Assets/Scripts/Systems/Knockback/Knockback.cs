using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : MonoBehaviour
{
    private static Knockback instance;
    public static Knockback Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Knockback>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void PerformKnockbackBetweenTwoPoints(Collider affectedCollider, Vector3 originOfForcePosition, float knockbackForce)
    {
        // Check if the collider has an attached rigidbody
        if (affectedCollider.TryGetComponent(out Rigidbody affectedRigidBody))
        {
            // Get the affected collider's position
            Vector3 affectedColliderPosition = affectedRigidBody.position;

            // Get the direction of the force
            Vector3 directionOfForce = affectedColliderPosition - originOfForcePosition;
            directionOfForce.Normalize();

            // Disable conflicting components
            SetComponentState(affectedCollider, false);

            // Apply the force to the object
            affectedRigidBody.AddForce(directionOfForce * knockbackForce, ForceMode.VelocityChange);
        }
        else
        {
            // Print a log
            Debug.LogWarning("The collider has no attached Rigidbody Component.");
        }
    }

    public void PerformKnockbackBasedOnDirection(Rigidbody affectedRigidbody, Vector3 directionOfForce, float knockBackForce)
    {
        // Normalize the direction if necessary
        if (directionOfForce.magnitude != 1)
            directionOfForce.Normalize();

        // Disable conflicting components
        SetComponentState(affectedRigidbody, false);

        // Apply the force to the object
        affectedRigidbody.AddForce(directionOfForce * knockBackForce, ForceMode.VelocityChange);
    }

    public void PerformKnockbackUpwards(Collider affectedCollider, float knockbackForce)
    {
        // Check if the collider has an attached rigidbody
        if (affectedCollider.TryGetComponent(out Rigidbody affectedRigidBody))
        {
            // Disable conflicting components
            SetComponentState(affectedCollider, false);

            // Apply the force to the object
            affectedRigidBody.AddForce(Vector3.up * knockbackForce, ForceMode.VelocityChange);
        }
        else
        {
            // Print a log
            Debug.LogWarning("The collider has no attached Rigidbody Component.");
        }
    }

    public void SetComponentState(Collider affectedCollider, bool activeState)
    {
        if (affectedCollider.TryGetComponent(out Animator affectedAnimator))
        {
            affectedAnimator.enabled = activeState;
        }

        if (affectedCollider.TryGetComponent(out NavMeshAgent affectedNavMeshAgent))
        {
            affectedNavMeshAgent.enabled = activeState;
        }
    }

    public void SetComponentState(Rigidbody affectedRigidbody, bool activeState)
    {
        if (affectedRigidbody.TryGetComponent(out Collider affectedCollider))
        {
            SetComponentState(affectedCollider, activeState);
        }
        else
        {
            Debug.LogWarning($"{affectedRigidbody.gameObject.name} does not have a collider component.");
        }
    }
}
