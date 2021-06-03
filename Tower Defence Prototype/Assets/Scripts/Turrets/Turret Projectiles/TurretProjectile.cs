using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private float travelSpeed;
    private int damage;
    private Transform target;


    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public Transform Target
    {
        get
        {
            return Target;
        }
        set
        {
            Target = value;
        }
    }



    public virtual void Hit()
    {
        Debug.LogError("Override Hit method has not been created for projectile");
    }
    private void Update()
    {
        Vector3.MoveTowards(target.transform.position, transform.position, travelSpeed * Time.deltaTime);
    }

}
