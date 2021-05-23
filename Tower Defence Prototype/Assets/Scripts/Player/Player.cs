using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance { get; private set; }

    [Header("Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegen;


    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;


    [Header("Inventory")]
    [SerializeField] private Canvas inventory;
    private bool isInventoryVisible;


    private float currentHealth;
    private float currentMana;

    public float MaxMana
    {
        get
        {
            return maxMana;
        }
    }
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
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }
    public Image ManaBar
    {
        get
        {
            return manaBar;
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
        //if current mana is less that max mana
        //increases by mana regen * time.delta time
        //clamp at max mana

        if (currentMana < maxMana)
        {
            currentMana += manaRegen * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
            manaBar.fillAmount = currentMana / maxMana;
        }


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
