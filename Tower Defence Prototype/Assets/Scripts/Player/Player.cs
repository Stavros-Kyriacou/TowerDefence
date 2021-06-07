using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    [Header("Stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float healthRegen;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegen;

    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;

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

        currentHealth = maxHealth;                                                      //initialize health
        currentMana = maxMana;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegen * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
            manaBar.fillAmount = currentMana / maxMana;
        }
    }
    public void TakeDamage(int damageAmount)
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
