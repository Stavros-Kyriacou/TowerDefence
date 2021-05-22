using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerAttack : MonoBehaviour
{
    [Header("Spell List")]
    [SerializeField] private List<SpellBase> spellList = new List<SpellBase>();

    //cache components
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
            ManaCheck(0);
        }
        else if (Input.GetKeyDown(KeyCode.W) && canCast)
        {
            ManaCheck(1);
        }
        else if (Input.GetKeyDown(KeyCode.E) && canCast)
        {
            ManaCheck(2);
        }
        else if (Input.GetKeyDown(KeyCode.R) && canCast)
        {
            ManaCheck(3);
        }
    }
    private void ManaCheck(int spellIndex)
    {
        if (Player.Instance.CurrentMana >= spellList[spellIndex].ManaCost)
        {
            Player.Instance.CurrentMana -= spellList[spellIndex].ManaCost;
            CastSpell(spellIndex);
        }
        else
        {
            Debug.Log($"You don't have enough mana for {spellList[spellIndex]}");
        }
    }
    private void CastSpell(int index)
    {
        //if the provided index is within range of the list, cast the spell and start cast time
        if (index >= 0 && index < spellList.Count)
        {
            spellList[index].CastSpell(GetMouseWorldPos(), transform.position);
            StartCoroutine(CastTime(spellList[index].CastTime));
        }
        else
        {
            Debug.LogError("Spell Index is not in range of list");
        }
    }
    IEnumerator CastTime(float castTime)
    {
        canCast = false;                                                                                //cant cast any other spells or move during cast time
        aiPath.canMove = false;
        playerMovement.Target.transform.position = Player.Instance.transform.position;                  //set movement target location to current pos so that you dont auto walk to previous mouse click after cast time
        yield return new WaitForSeconds(castTime);

        aiPath.canMove = true;                                                                          //after cast time is done, allow casting and moving
        canCast = true;
    }
    private Vector2 GetMouseWorldPos()
    {
        Vector3 mousePos3D = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(mousePos3D.x, mousePos3D.y);
        return mousePos;
    }
    private void ShootFireBall()
    {
        /*StartCoroutine(CastTime(fireballCastTime));

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
        fireball.Destination = fireballDestination;*/
    }
}
