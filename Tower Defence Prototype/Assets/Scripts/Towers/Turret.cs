using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private int cost;
    [SerializeField] private float attackRange;
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private float damage;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    public int Cost
    {
        get
        {
            return cost;
        }
    }
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }
    public float AttacksPerSecond
    {
        get
        {
            return 1 / attacksPerSecond;
        }
    }
    public float Damage
    {
        get
        {
            return damage;
        }
    }
}
