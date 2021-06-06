using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTurret : Turret
{
    public override void Shoot()
    {
        TrackingProjectile cannonBall = Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as TrackingProjectile;
        cannonBall.Damage = GetDamageInstance();
        cannonBall.Target = Target.transform;
    }
}
