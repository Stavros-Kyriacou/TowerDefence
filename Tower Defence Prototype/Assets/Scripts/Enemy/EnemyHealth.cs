using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private BuildingManager buildingManager;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    private float currentHealth;
    [SerializeField] private int minMaterialDrop;
    [SerializeField] private int maxMaterialDrop;
    private int materialDrop;

    [Header("Effects")]
    [SerializeField] private GameObject deathEffect;

    [Header("UI")]
    [SerializeField] private Image healthBar;

    private string lastDamageSource;

    private void Awake()
    {
        buildingManager = BuildingManager.Instance;
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageToTake, string source)
    {
        lastDamageSource = source;
        currentHealth -= damageToTake;

        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Death effect not set on enemy Name: " + gameObject.name);
            Destroy(gameObject);
        }
        if (lastDamageSource != "Crystal")
        {
            BuildingManager.Instance.AddMaterials(UnityEngine.Random.Range(minMaterialDrop, maxMaterialDrop + 1));
        }
    }
}
