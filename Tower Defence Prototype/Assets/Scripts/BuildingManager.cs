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
    [SerializeField] LayerMask turretLayerMask;

    [Header("Materials")]
    [SerializeField] private TextMeshProUGUI materialsText;
    [SerializeField] private int startMaterials;
    private int currentMaterials;
    private int selectedTurretCost;

    [Header("HUD")]
    [SerializeField] private List<TextMeshProUGUI> turretCosts;

    Vector3Int currentGridPos;
    Vector3Int previousGridPos;
    [SerializeField] Collider2D[] hit;

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
        materialsText.text = currentMaterials.ToString();

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
        mouseInput.Mouse.MouseClick.performed += _ => BuildTurret();
        mouseInput.Mouse.CancelBuild.performed += _ => CancelBuild();
        keyInput.Keyboard.Turret1.performed += _ => BuildTurret(0);
        keyInput.Keyboard.Turret2.performed += _ => BuildTurret(1);
        keyInput.Keyboard.Turret3.performed += _ => BuildTurret(2);
        keyInput.Keyboard.Turret4.performed += _ => BuildTurret(3);
        keyInput.Keyboard.Turret5.performed += _ => BuildTurret(4);
    }
    private void Update()
    {
        if (!canBuild)
        {
            return;
        }
        else
        {
            //convert mouse pos to tilemap grip position
            Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = mainCam.ScreenToWorldPoint(mousePosition);
            currentGridPos = wallTileMap.WorldToCell(mousePosition);

            if (previousGridPos != currentGridPos && wallTileMap.HasTile(currentGridPos))
            {
                //clear previous highighted tile. hightlight current tile. update previous grip position
                highlightTileMap.SetTile(previousGridPos, null);
                highlightTileMap.SetTile(currentGridPos, highlightTile);
                previousGridPos = currentGridPos;

                //dont think this does anything?? idk i cant tell :^)
                // if (!wallTileMap.HasTile(wallTileMap.WorldToCell(mousePosition)))
                // {
                //     highlightTileMap.SetTile(previousGridPos, null);
                // }
            }
            else if (!wallTileMap.HasTile(currentGridPos))
            {
                //if you move your mouse away from the wall tiles, remove the highlighted tile
                highlightTileMap.SetTile(previousGridPos, null);

                //this fixes a bug where if you move your cursor away from a wall tile and then move it back to the same position. it will re highlight the tile again
                currentGridPos = new Vector3Int(0, 0, 0);
                previousGridPos = new Vector3Int(0, 0, 0);
            }
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
    private void BuildTurret()
    {
        Debug.Log("Build Turret :)");
        if (canBuild && selectedTurret != null && selectedTurretCost <= currentMaterials)
        {
            //Convert mouse position to tilemap grid position
            Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = mainCam.ScreenToWorldPoint(mousePosition);
            Vector3Int gridPosition = wallTileMap.WorldToCell(mousePosition);

            //Raycast to see if a turret has already been built at that spot
            Vector2 rayStartPos = new Vector2(gridPosition.x + 0.5f, gridPosition.y + 0.5f);
            Vector2 rayDir = new Vector2(1, 0);
            RaycastHit2D hitTurret = Physics2D.Raycast(rayStartPos, rayDir, 0.1f, turretLayerMask);

            //if there is a wall tile in the current spot and a turret hasnt been built
            if (wallTileMap.HasTile(gridPosition) && !hitTurret)
            {
                //remove the highlighted tile
                highlightTileMap.SetTile(currentGridPos, null);

                //stop player from moving to the position
                playerMovement.CanMove = false;
                playerMovement.Destination = playerMovement.transform.position;

                //apply turret cost and spawn it on the tile
                SubtractMaterials(selectedTurretCost);
                Instantiate(selectedTurret, (Vector3)gridPosition + turretOffset, transform.rotation);

                //re-enable player movement
                canBuild = false;
                playerMovement.CanMove = true;
            }
        }
    }
    private void CancelBuild()
    {
        Debug.Log("Cancel Build :)");
        canBuild = false;
        selectedTurret = null;
        highlightTileMap.SetTile(previousGridPos, null);
        currentGridPos = new Vector3Int(0, 0, 0);
        previousGridPos = new Vector3Int(0, 0, 0);
    }
    private void Turret1()
    {
        Debug.Log("Select turret 1");
        canBuild = true;
        selectedTurret = turretPrefabs[0];
        selectedTurretCost = turrets[0].Cost;
    }
    private void BuildTurret(int type)
    {
        switch (type)
        {
            case 0:
                Debug.Log("Turret 1 Selected");
                break;
            case 1:
                Debug.Log("Turret 2 Selected");
                break;
            case 2:
                Debug.Log("Turret 3 Selected");
                break;
            case 3:
                Debug.Log("Turret 4 Selected");
                break;
            case 4:
                Debug.Log("Turret 5 Selected");
                break;
            default:
                Debug.Log("No Turret Selected, Defaul Case");
                break;
        }
        Debug.Log("Select turret 1");
        canBuild = true;
        selectedTurret = turretPrefabs[type];
        selectedTurretCost = turrets[type].Cost;
    }
}
