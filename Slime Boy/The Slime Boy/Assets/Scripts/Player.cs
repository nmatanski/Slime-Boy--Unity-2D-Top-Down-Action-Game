using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D rb;

    private Vector2 moveAmmount;

    private Animator animator;

    private int timer = 0;

    [SerializeField]
    private float speed;


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isRunningLeft", false);
            animator.SetBool("isRunningRight", false);
            animator.SetBool("isRunningUp", false);
            animator.SetBool("isRunningDown", false);

            if (moveInput == Vector2.left)
            {
                animator.SetBool("isRunningLeft", true);
            }
            else if (moveInput == Vector2.right)
            {
                animator.SetBool("isRunningRight", true);
            }
            else if (moveInput == Vector2.up)
            {
                animator.SetBool("isRunningUp", true);
            }
            else if (moveInput == Vector2.down)
            {
                animator.SetBool("isRunningDown", true);
            }
        }
        else
        {
            timer++;
            if (timer > 10)
            {
                animator.SetBool("isRunningLeft", false);
                animator.SetBool("isRunningRight", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isRunningDown", false);
                timer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmmount * Time.deltaTime);
    }
}
