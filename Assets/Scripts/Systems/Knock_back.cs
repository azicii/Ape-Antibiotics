using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock_back : MonoBehaviour
{
    private static Knock_back instance;
    public static Knock_back Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Knock_back>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Test()
    {
        Debug.Log("It worked!");
    }

    public void ApplyKnockback(Collider affectedCollider, Vector3 originOfForcePosition, float forceAppliedAmount)
    {
        // Collect the Rigidbody
        if (affectedCollider.TryGetComponent<Rigidbody>(out Rigidbody affectedRigidbody))
        {
            // Get the affected collider's position
            Vector3 affectedPosition = affectedCollider.transform.position;

            // Calculate the difference of the origin and the affected collider's position
            Vector3 affectedDirectionVector = affectedPosition - originOfForcePosition;
            affectedDirectionVector.Normalize();

            // Applying force to the affected rigidbody
            affectedRigidbody.AddForce(affectedDirectionVector * forceAppliedAmount, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning($"No Rigidbody was on {affectedCollider.gameObject.name}");
        }
    }
}
