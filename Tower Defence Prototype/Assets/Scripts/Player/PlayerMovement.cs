using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AIDestinationSetter destinationSetter;
    private new Camera camera;

    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;
    private float moveDirectionX;
    private float moveDirectionY;

    private Vector2 mousePos;
    [SerializeField] private Transform target;


    public Transform Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            target.transform.position = mousePos;
            SetDestination(target.transform);
        }

        /*moveDirectionX = Input.GetAxisRaw("Horizontal");
        moveDirectionY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveDirectionX, moveDirectionY).normalized;

        animator.SetFloat("dirX", moveDirectionX);

        FlipSprite();*/
    }


    private void FixedUpdate()
    {
        rigidBody.velocity = moveDirection * moveSpeed;
        if (rigidBody.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    private void FlipSprite()
    {
        /*if(deltaX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }*/
    }
    private void SetDestination(Transform targetPos)
    {
        destinationSetter.target = targetPos;
    }
}
