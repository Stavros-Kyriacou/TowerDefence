using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance { get; private set; }

    //Inventory
    [SerializeField] private Canvas inventory;
    private bool isInventoryVisible;

    //Stats
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;

    private float currentHealth;
    private float currentMana;

    public float CurrentMana
    {
        get
        {
            return currentMana;
        }
        set
        {
            currentMana = value;
        }
    }
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    private void Awake()
    {
        Instance = this;                                                                //singleton, this is the only player

        isInventoryVisible = false;                                                     //hide inventory
        inventory.enabled = false;

        currentHealth = maxHealth;                                                      //initialize health
        currentMana = maxMana;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }
    private void ToggleInventory()
    {
        if (isInventoryVisible)
        {
            isInventoryVisible = false;
            inventory.enabled = false;
        }
        else
        {
            isInventoryVisible = true;
            inventory.enabled = true;
        }
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("You died");
    }
}
