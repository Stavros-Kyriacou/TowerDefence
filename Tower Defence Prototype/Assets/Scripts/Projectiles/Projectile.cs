using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }
    public float ProjectileDamage { get; set; }
}
