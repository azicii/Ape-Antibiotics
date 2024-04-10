using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knockback : MonoBehaviour
{
    [SerializeField] public bool knockback = true;
    [SerializeField] float knockbackMultiplier = 1f;
    [SerializeField] float upwardsForce = 10f;

    [SerializeField] float timeToReposition = 2f;

    public IEnumerator  (Collider enemy, Vector3 knockbackPoint, float knockbackDamage)
    {
        if (knockback)
        {
            Debug.Log("knockback applied");

            var nav = enemy.GetComponent<NavMeshAgent>();
            var anim = enemy.GetComponent<Animator>();
            var rb = enemy.GetComponent<Rigidbody>();
            var en = enemy.GetComponent<EnemyMovement>();

            anim.enabled = false;
            nav.enabled = false;
            en.enabled = false;
            rb.AddExplosionForce(knockbackDamage * knockbackMultiplier, knockbackPoint, knockbackDamage, upwardsForce);

            yield return new WaitForSeconds(timeToReposition);

            en.enabled = true;
            anim.enabled = true;
            anim.enabled = true;
        }
    }
}
