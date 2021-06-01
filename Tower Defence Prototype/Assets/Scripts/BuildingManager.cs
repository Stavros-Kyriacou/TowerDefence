using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    private MouseInput mouseInput;
    private KeyboardInput keyInput;
    public Tilemap tilemap;
    private GameObject selectedTurret;
    [SerializeField] private List<GameObject> turretPrefabs;
    [SerializeField] private Vector3 turretOffset;

    private void OnEnable()
    {
        mouseInput.Enable();
        keyInput.Enable();
    }
    private void OnDisable()
    {
        mouseInput.Disable();
        keyInput.Disable();
    }
    private void Awake()
    {
        mouseInput = new MouseInput();
        keyInput = new KeyboardInput();
    }
    private void Start()
    {
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
        keyInput.Keyboard.Turret1.performed += _ => Turret1();
    }
    private void MouseClick()
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

        if (selectedTurret != null)
        {
            if (tilemap.HasTile(gridPosition))
            {
                Debug.Log("yes");
                Instantiate(selectedTurret, (Vector3)gridPosition + turretOffset, transform.rotation);
            }
        }
    }
    private void Turret1()
    {
        Debug.Log("Selected Turret1");
        selectedTurret = turretPrefabs[0];
    }

}
