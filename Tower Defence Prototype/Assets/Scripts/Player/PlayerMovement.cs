using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;
    private float moveDirectionX;
    private float moveDirectionY;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        moveDirectionY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveDirectionX, moveDirectionY).normalized;

        animator.SetFloat("dirX", moveDirectionX);

        FlipSprite();
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
}
