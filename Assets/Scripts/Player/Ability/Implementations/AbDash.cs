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
            Vector3 forward = parent.transform.forward;

            parentRigidbody.AddForce(forward * force, ForceMode.VelocityChange);
        }
    }
}
