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
    private bool canBuild;

    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

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
    }
    private void Start()
    {
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
        keyInput.Keyboard.Turret1.performed += _ => Turret1();
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
        //check if turret != null
        //check if you have enough mats
        if (selectedTurret != null && selectedTurretCost <= currentMaterials)
        {
            Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            if (tilemap.HasTile(gridPosition) && canBuild)
            {
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
