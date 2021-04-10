using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : SpellBase
{
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    private Vector3 destination;
    private bool reachedDestination;

    //Properties
    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }
    public Vector3 Destination
    {
        get
        {
            return destination;
        }
        set
        {
            destination = value;
        }
    }
    public bool ReachedDestination
    {
        get
        {
            return reachedDestination;
        }
        set
        {
            reachedDestination = value;
        }
    }
}
