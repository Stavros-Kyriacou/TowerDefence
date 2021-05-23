using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth;
    private float currentHealth;

    [Header("Effects")]
    [SerializeField] private GameObject deathEffect;

    [Header("UI")]
    [SerializeField] private Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        healthBar.fillAmount = currentHealth / maxHealth;

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
