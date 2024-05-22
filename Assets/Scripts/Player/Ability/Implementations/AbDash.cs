using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Abilities/Dash")]
public class AbDash : AbilityBase
{
    [SerializeField] float force;

    public override void Perform(GameObject parent)
    {
        if (parent.TryGetComponent(out Rigidbody parentRigidbody))
        {
            // Calculate the direction
            Vector3 forward = parent.GetComponentInChildren<Camera>().transform.forward;

            // Directly change the player's velocity
            parentRigidbody.velocity = Vector3.zero;
            parentRigidbody.velocity = forward * force;
        }
    }
}
