using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : Turret
{
    [Header("Ice Turret")]
    [Range(0, 1)] [SerializeField] private float slowPercent;
    [SerializeField] private float slowDuration = 1f;
    [SerializeField] private float slowUpdateRate = 0.3f;


    private void Start() {
        StartCoroutine(FindEnemies());
    }
    private IEnumerator FindEnemies()
    {
        while (true)
        {
            var enemies = Physics2D.OverlapCircleAll(transform.position, AttackRange, TargetLayerMask);
            SlowAllEnemies(enemies);
            Debug.Log(enemies.Length);
            yield return new WaitForSeconds(slowUpdateRate);
        }
    }
    private void SlowAllEnemies(Collider2D[] enemies)
    {
        foreach (var enemy in enemies)
        {
            var e = enemy.GetComponent<ISlow>();
            e.ApplySlow(slowPercent, slowDuration);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Enemy enemy = other.GetComponentInParent<Enemy>();
    //     if (enemy != null)
    //     {
    //         enemy.ApplySlow(slowPercent, slowDuration);
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     Enemy enemy = other.GetComponentInParent<Enemy>();
    //     if (enemy != null)
    //     {
    //         enemy.RemoveSlow();
    //     }
    // }
}
