using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    [Header("Fireball")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float moveSpeed;

    [Header("Fireball Explosion")]
    [SerializeField] private float explosionRadius;
    [SerializeField] private int explosionDamage;
    [SerializeField] private LayerMask enemyLayerMask;

    [Header("Audio")]
    [SerializeField] private bool playSound;
    [SerializeField] private AudioClip explodeSFX;

    private Vector3 destination;
    private bool reachedDestination;
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
    /*public float AttacksPerSecond
    {
        get
        {
            return  1 / attacksPerSecond;
        }
    }*/
    /*public float MinAttacksPerSecond
    {
        get
        {
            return minAttacksPerSecond;
        }
    }*/

    private void Start()
    {
        if (explodeSFX != null && playSound)
        {
            AudioSource.PlayClipAtPoint(explodeSFX, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z), 0.1f);
        }
    }
    void Update()
    {
        if (!reachedDestination && destination != null)
        {
            //have not reached destination, move towards destination
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

            if (transform.position == destination)
            {
                reachedDestination = true;
                Explode();
            }
        }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(destination, explosionRadius, enemyLayerMask);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            var enemy = enemiesToDamage[i].GetComponent<EnemyHealth>();
            enemy.TakeDamage(ProjectileDamage);
        }

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
