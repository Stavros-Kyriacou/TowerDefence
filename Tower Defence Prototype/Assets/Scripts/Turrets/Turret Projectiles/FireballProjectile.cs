using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : TrackingProjectile
{
    [Header("Fireball")]
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask targetLayerMask;

    public override void Hit()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayerMask);

        foreach (Collider2D enemy in enemiesToDamage)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable.TakeDamage(Damage);
        }
        Destroy(gameObject);    
    }
    
}
