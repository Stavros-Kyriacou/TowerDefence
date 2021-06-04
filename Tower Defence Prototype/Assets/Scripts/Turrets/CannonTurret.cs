using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTurret : Turret
{
    public override void Shoot()
    {
        TurretProjectile cannonBall = Instantiate(ProjectilePrefab, FirePoint.transform.position, Quaternion.identity) as TurretProjectile;
        cannonBall.Damage = GetDamageInstance();
        cannonBall.Target = Target.transform;
    }
}
