using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKnockback : MonoBehaviour
{
    public float knockbackAmount = 1000f;
    public float crackedSheildKnockbackMultiplier = 5f;

    private Rigidbody rb;
    private Shield shield;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shield = GetComponent<Shield>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Hit enemy");
            Vector3 hitDirection =  gameObject.transform.position - collision.gameObject.transform.position;
            hitDirection.Normalize();

            Debug.Log(hitDirection);
            hitDirection.y = 5;


            if (shield.increasedKnockback)
            {
                rb.AddForce(hitDirection * knockbackAmount * crackedSheildKnockbackMultiplier);
            }
            else
            {
                rb.AddForce(hitDirection * knockbackAmount);
            }
        }
    }
}
