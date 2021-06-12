using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMouseHover : MonoBehaviour
{
    private TurretStatScreen turretStats;
    private float hoverTime = 0.3f;
    private float hoverCounter = 0;
    private bool isHovering;

    private void Start()
    {
        turretStats = BuildingManager.Instance.GetComponent<TurretStatScreen>();
    }
    private void Update()
    {
        //Turret stat mouse hover delay
        if (isHovering && hoverCounter < hoverTime)
        {
            hoverCounter += Time.deltaTime;
        }
        if (hoverCounter >= hoverTime)
        {
            if (turretStats == null)
            {
                return;
            }
            else
            {
                Turret turretComponent = GetComponent<Turret>();

                if (turretComponent == null)
                {
                    //check if the turret component is in the parent object, this is for the ice turret
                    turretComponent = GetComponentInParent<Turret>();
                }

                if (turretComponent != null)
                {
                    turretStats.Target = turretComponent;
                    turretStats.UpdateStats();
                }
            }
        }
    }
    private void OnMouseEnter()
    {
        isHovering = true;
    }
    private void OnMouseExit()
    {
        isHovering = false;
        hoverCounter = 0f;
        turretStats.Target = null;
        turretStats.StatScreen.SetActive(false);
    }
}

