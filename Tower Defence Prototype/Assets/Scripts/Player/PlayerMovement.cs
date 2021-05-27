using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float movementSpeed;
    [SerializeField] private LayerMask collisionLayerMask;
    private MouseInput mouseInput;
    private Vector2 destination;
    private CircleCollider2D circleCollider;
    [SerializeField] private float colliderBuffer;
    private float colliderRadius;
    private bool canMove;

    public Vector2 Destination
    {
        get
        {
            return destination;
        }
        set
        {
            destination = value;
        }
    }
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }
    private void Awake()
    {
        mouseInput = new MouseInput();
        circleCollider = GetComponent<CircleCollider2D>();
        colliderRadius = circleCollider.radius;
    }
    private void OnEnable()
    {
        mouseInput.Enable();
    }
    private void OnDisable()
    {
        mouseInput.Disable();
    }
    private void Start()
    {
        canMove = true;
        destination = transform.position;
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
    }
    private void MouseClick()
    {
        if (canMove)
        {
            //get the mouse position in world space
            Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            //get the distance and direction of the mouse from the player
            float mouseDistance = Vector2.Distance(mousePosition, transform.position);
            Vector2 mouseDirection = (mousePosition - (Vector2)transform.position).normalized;

            //draw ray from player to mouse location
            //OPTIONAL add the buffer distance to the diestination of the ray to stop getting extremely close to a wall and bouncing
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseDirection, mouseDistance, collisionLayerMask);
            Debug.DrawRay(transform.position, mouseDirection * mouseDistance, Color.red);

            if (hit.collider != null)
            {
                //get the distance and direction of the hit from the player
                float distance = Vector2.Distance(hit.point, transform.position);
                Vector2 direction = (hit.point - (Vector2)transform.position).normalized;

                //account for the collider and buffer disttance
                float fixedDistance = distance - (colliderRadius + colliderBuffer);

                //set destination
                destination = (Vector2)transform.position + (fixedDistance * direction);
            }
            else
            {
                destination = mousePosition;
            }
        }
    }
    private void Update()
    {
        // transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        }
    }
}
