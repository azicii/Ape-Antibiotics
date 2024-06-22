using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] float timeBeforeDestroy = 3f;
    Animator animator;
    public Image healthBar;
    public float timeBeforeReposition = 5f;

    bool isDead = false;

    GameManager gameManager;

    public bool IsDead()
    {
        return isDead;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Update()
    {
        healthBar.fillAmount = hitPoints / 100;
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        //Debug.Log($"{this.name} health: {hitPoints}");

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameManager.EnemyDefeated();

        if (isDead) return;

        isDead = true;
        GetComponent<NavMeshAgent>().enabled = false;
        animator.SetTrigger("die");
        Destroy(gameObject, timeBeforeDestroy);
    }
}
