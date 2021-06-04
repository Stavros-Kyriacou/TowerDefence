using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    private PlayerMovement playerMovement;
    private MouseInput mouseInput;
    private KeyboardInput keyInput;
    [SerializeField] private Camera mainCam;
    private bool canBuild;

    [Header("Tilemap")]
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Tilemap highlightTileMap;
    [SerializeField] private Tile highlightTile;

    [Header("Turrets")]
    [SerializeField] private List<GameObject> turretPrefabs;
    [SerializeField] private Vector3 turretOffset;
    private GameObject selectedTurret;
    private List<Turret> turrets = new List<Turret>();

    [Header("Materials")]
    [SerializeField] private TextMeshProUGUI materialsText;
    [SerializeField] private int startMaterials;
    private int currentMaterials;
    private int selectedTurretCost;

    [Header("HUD")]
    [SerializeField] private List<TextMeshProUGUI> turretCosts;

    Vector3Int currentGridPos;
    Vector3Int previousGridPos;

    public int CurrentMaterials
    {
        get
        {
            return currentMaterials;
        }
        set
        {
            currentMaterials = value;
        }
    }
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
        Instance = this;
        mouseInput = new MouseInput();
        keyInput = new KeyboardInput();
        playerMovement = Player.Instance.GetComponent<PlayerMovement>();
        currentMaterials = startMaterials;

        foreach (GameObject go in turretPrefabs)
        {
            Turret t = go.GetComponent<Turret>();
            turrets.Add(t);
        }

        for (int i = 0; i < turretCosts.Count; i++)
        {
            turretCosts[i].text = turretPrefabs[i].GetComponent<Turret>().Cost.ToString();
        }
    }
    private void Start()
    {
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
        keyInput.Keyboard.Turret1.performed += _ => Turret1();
    }
    private void Update() 
    {
        if (canBuild)
        {
            //if walls tile map has a tile at grid position
                //set highlight tile map to highlighted tile
            
            //if current highlighted tile is at different position to previous
                //set previous tile to null

            Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = mainCam.ScreenToWorldPoint(mousePosition);
            currentGridPos = wallTileMap.WorldToCell(mousePosition);

            if (previousGridPos != currentGridPos && wallTileMap.HasTile(currentGridPos))
            {
                highlightTileMap.SetTile(previousGridPos, null);
                highlightTileMap.SetTile(currentGridPos, highlightTile);
                previousGridPos = currentGridPos;

                if (!wallTileMap.HasTile(wallTileMap.WorldToCell(mousePosition)))
                {   
                    highlightTileMap.SetTile(previousGridPos, null);
                }
            }
            else if (!wallTileMap.HasTile(currentGridPos))
            {
                //if the current mouse position is different to the currentGridPos and the wallTileMap does not have a tile
                // Debug.Log("No wall tile at current posistion");
                highlightTileMap.SetTile(previousGridPos, null);
            }
            else if (wallTileMap.WorldToCell(mousePosition) != previousGridPos)
            {
                Debug.Log("Mouse pos is different to currentGridPos");
            }

            // Debug.Log("Current pos: " + currentGridPos + " || Previous pos: " + previousGridPos);

        }
    }
    public void AddMaterials(int amount)
    {
        currentMaterials += amount;
        materialsText.text = currentMaterials.ToString();
    }
    public void SubtractMaterials(int amount)
    {
        currentMaterials -= amount;
        materialsText.text = currentMaterials.ToString();
    }
    private void MouseClick()
    {
        if (selectedTurret != null && selectedTurretCost <= currentMaterials)
        {
            Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = mainCam.ScreenToWorldPoint(mousePosition);
            Vector3Int gridPosition = wallTileMap.WorldToCell(mousePosition);

            if (wallTileMap.HasTile(gridPosition) && canBuild)
            {
                highlightTileMap.SetTile(currentGridPos, null);
                playerMovement.CanMove = false;
                playerMovement.Destination = playerMovement.transform.position;
                SubtractMaterials(selectedTurretCost);
                Instantiate(selectedTurret, (Vector3)gridPosition + turretOffset, transform.rotation);
                canBuild = false;
                playerMovement.CanMove = true;
            }
        }
    }
    private void Turret1()
    {
        canBuild = true;
        selectedTurret = turretPrefabs[0];
        selectedTurretCost = turrets[0].Cost;
    }
}
