using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : Turret
{
    [Header("Ice Turret")]
    [Range(0, 1)] [SerializeField] private float slowPercent;
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.Slow(slowPercent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.RemoveSlow();
        }
    }
}
