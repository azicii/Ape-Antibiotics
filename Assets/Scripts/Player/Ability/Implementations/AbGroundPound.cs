using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundPound", menuName = "Abilities/GroundPound")]
public class AbGroundPound : AbilityBase
{
    [Header("Specific Attributes")]

    [Tooltip("How much damage the ability does.")]
    [SerializeField] float damage;

    [Tooltip("How much force the ability applies.")]
    [SerializeField] float force;

    [Tooltip("The minimum angle the arc must be to perform the ability.")]
    [SerializeField] float minAngleOfJump = 30f;

    public override void Perform(GameObject parent)
    {
        if (parent.TryGetComponent(out Rigidbody parentRigidbody))
        {
            // Get the camera's forward direction and position
            Vector3 cameraForward = parent.GetComponentInChildren<Camera>().transform.forward;

            // Determine if the first position's angle to the player is at least minumum
            Vector3 directionOfJump;
            if (Vector3.Angle(parent.transform.forward, cameraForward) > minAngleOfJump && cameraForward.y > 0f)
            {
                directionOfJump = cameraForward;
            }
            else
            {
                // Calculate the direction with the minimum angle upwards
                float radians = Mathf.Deg2Rad * minAngleOfJump;
                Vector3 adjustedDirection = cameraForward;
                adjustedDirection.y = Mathf.Tan(radians);
                directionOfJump = adjustedDirection.normalized;
            }

            Debug.Log(directionOfJump);

            // Apply the force
            Knockback.Instance.PerformKnockbackBasedOnDirection(parentRigidbody, directionOfJump, force);
        }
        else
        {
            Debug.LogWarning("Couldn't get the player's Rigidbody.");
        }
    }
}
