using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    private float destination;
    private bool reachedDestination;

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }
    public float ProjectileDamage { get; set; }
}
