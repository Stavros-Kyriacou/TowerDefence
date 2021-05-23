using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStorm : GroundSpell
{
    [Header("Lightning Storm")]
    [SerializeField] private LightningStorm myPrefab;
    [SerializeField] private ParticleSystem pulseParticle;
    [Header("DamageOverTime")]
    [SerializeField] private float hitRate;


    private void Awake()
    {
        InvokeRepeating("DealDamage", 0f, hitRate);
    }
    private void DealDamage()
    {
        pulseParticle.Play();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, SpellRadius, TargetLayerMask);               //array of all enemies in the spell radius
        var damageInstance = GetDamageInstance();                                                                                     //calculate damage for this particular spell hit

        for (int i = 0; i < enemiesToDamage.Length; i++)                                                                            //apply damage to all hit enemies
        {
            var enemy = enemiesToDamage[i].GetComponent<EnemyHealth>();
            enemy.TakeDamage(damageInstance);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpellRadius);
    }
    public override void CastSpell(Vector2 mousePos, Vector2 playerPos)
    {
        var mouseDirection = (mousePos - playerPos).normalized;
        var mousePlayerDistance = Vector2.Distance(playerPos, mousePos);

        if (mousePlayerDistance > CastRange)
        {
            PlacementLocation = (mouseDirection * CastRange) + playerPos; 
        }
        else
        {
            PlacementLocation = mousePos;
        }

        LightningStorm lightningStorm = Instantiate(myPrefab, PlacementLocation, myPrefab.transform.rotation);
    }
}
