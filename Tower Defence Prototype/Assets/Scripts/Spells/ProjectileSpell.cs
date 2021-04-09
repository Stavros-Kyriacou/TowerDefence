using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : SpellBase
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
}
