using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float currentHealth;

    [SerializeField] private GameObject deathEffect;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if(deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Death effect not set on enemy Name: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
