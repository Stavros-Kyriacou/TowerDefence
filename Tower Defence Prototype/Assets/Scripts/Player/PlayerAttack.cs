using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerAttack : MonoBehaviour
{
    [Header("FireBall")]
    [SerializeField] private Fireball fireballPrefab;
    [SerializeField] private float maxTravelRange;
    [SerializeField] private float fireballDamage;
    [SerializeField] private float fireballCastTime;

    private Camera mainCam;
    private PlayerMovement playerMovement;
    private AIPath aiPath;


    private bool canCast = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        aiPath = GetComponent<AIPath>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canCast)
        {
            ShootFireBall();
        }
    }

    private void ShootFireBall()
    {
        StartCoroutine(CastTime(fireballCastTime));

        //get the mouse position in world space
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePos2D = new Vector2(mousePos.x, mousePos.y);

        var playerPos = new Vector2(transform.position.x, transform.position.y);

        //get the normalized direction of the mouse
        var mouseDirection = (mousePos2D - playerPos).normalized;

        //get the distance between the player and mouse
        var distance = Vector2.Distance(transform.position, mousePos);

        //initialise variables
        var fireballDestination = new Vector3();


        if (distance > maxTravelRange)
        {
            //if mouse if outside of travel range, set the destination at the max range in the correct direction
            fireballDestination = (mouseDirection * maxTravelRange) + playerPos;
        }
        else
        {
            //otherwise if the mouse is inside the max range, just set the destination as the mousepos
            fireballDestination = mousePos2D;
        }

        //angle the fireball must rotate to face its target
        Vector3 dir = fireballDestination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //spawn the projectile
        Fireball fireball = Instantiate(fireballPrefab, Player.Instance.transform.position, Quaternion.AngleAxis(angle, Vector3.forward)) as Fireball;
        //fireball.ProjectileDamage = fireballDamage;
        fireball.Destination = fireballDestination;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxTravelRange);
    }
    IEnumerator CastTime(float castTime)
    {
        canCast = false;
        aiPath.canMove = false;
        playerMovement.Target.transform.position = Player.Instance.transform.position;
        yield return new WaitForSeconds(castTime);
        aiPath.canMove = true;
        canCast = true;
    }
    private Vector2 GetMousePos()
    {
        Vector3 mousePos3D = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
        return mousePos;
    }
}
