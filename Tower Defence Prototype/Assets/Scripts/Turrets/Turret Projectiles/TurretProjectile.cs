using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    [SerializeField] private int damage;
    private Transform target;
    public Vector3 destination;


    public int Damage { get { return damage; } set { damage = value; } }
    public Transform Target { get { return target; } set { target = value; } }




    public virtual void Hit()
    {
        Debug.LogError("Override Hit method has not been created for projectile");
    }
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, travelSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            IDamageable damageable = target.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
