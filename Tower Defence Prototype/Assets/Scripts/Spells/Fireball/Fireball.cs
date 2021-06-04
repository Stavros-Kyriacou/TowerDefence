using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ProjectileSpell
{
    [Header("Fireball Explosion")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Fireball myPrefab;
    [SerializeField] private float explosionRadius;

    private void Start()
    {
        if (HitSFX != null && PlayAudio)
        {
            AudioSource.PlayClipAtPoint(HitSFX, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z), 0.1f);
        }
    }
    void Update()
    {
        if (!ReachedDestination && Destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, ProjectileSpeed * Time.deltaTime);        //have not reached destination, move towards destination

            if (transform.position == Destination)                                                                              //explode once reached destination
            {
                ReachedDestination = true;
                Explode();
            }
        }
    }
    public override void CastSpell(Vector2 mousePos, Vector2 playerPos)
    {
        var mouseDirection = (mousePos - playerPos).normalized;
        var mousePlayerDistance = Vector2.Distance(playerPos, mousePos);
        Vector3 fireballDestination = new Vector3();

        if (mousePlayerDistance > TravelRange)
        {
            fireballDestination = (mouseDirection * TravelRange) + playerPos;
        }
        else
        {
            fireballDestination = mousePos;
        }

        Vector3 dir = fireballDestination - (Vector3)playerPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Fireball fireball = Instantiate(myPrefab, playerPos, Quaternion.AngleAxis(angle, Vector3.forward));
        fireball.Destination = fireballDestination;
    }
    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);                                   //OPTIMISATION dont spawn explosion, have it hidden initially as part of the same fireball prefab -> show it when fireball reached destination

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(Destination, explosionRadius, TargetLayerMask);               //array of all enemies in the explosion radius
        var damageInstance = GetDamageInstance();                                                                                 //calculate damage for this particular spell hit

        for (int i = 0; i < enemiesToDamage.Length; i++)                                                                        //apply damage to all hit enemies
        {
            var enemy = enemiesToDamage[i].GetComponent<EnemyHealth>();
            enemy.TakeDamage(damageInstance);
        }

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
