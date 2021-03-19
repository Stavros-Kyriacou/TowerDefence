using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private Canvas inventory;
    private bool isInventoryVisible;

    private void Awake()
    {
        Instance = this;
        isInventoryVisible = false;
        inventory.enabled = false;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }
    private void ToggleInventory()
    {
        if (isInventoryVisible)
        {
            isInventoryVisible = false;
            inventory.enabled = false;
        }
        else
        {
            isInventoryVisible = true;
            inventory.enabled = true;
        }
    }
}
