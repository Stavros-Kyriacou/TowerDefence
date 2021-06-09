using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballProjectile : TrackingProjectile
{
    public override void Hit()
    {
        IDamageable damageable = Target.gameObject.GetComponent<IDamageable>();
        damageable.TakeDamage(Damage);
        Destroy(gameObject);
    }
}
