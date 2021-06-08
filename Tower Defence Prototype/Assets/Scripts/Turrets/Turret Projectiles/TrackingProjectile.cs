using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    private int damage;
    private Transform target;
    private Vector3 destination;


    public int Damage { get { return damage; } set { damage = value; } }
    public Transform Target { get { return target; } set { target = value; } }




    public virtual void Hit()
    {
        Debug.LogError("Override Hit method has not been created for projectile");
    }
    private void Update()
    {
        //****Need to add rotation tracking of projectile****
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, travelSpeed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Hit();
        }
    }
}
