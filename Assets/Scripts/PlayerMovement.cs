using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    Animator animator;

    //string animationState = "AnimationState";

    string isrunning = "Isrunning";

    //enum CharStates
    //{

    //    walkEast = 1,
    //    walkWest = 2,
    //    walkNorth = 3,
    //    walkSouth = 4,
        
    //}

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from keyboard
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        movement.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        UpdateState(); // Flip the character

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateState()
    {

        if(Mathf.Approximately(movement.x,0) && Mathf.Approximately(movement.y, 0))
        { 
            animator.SetBool("Isrunning", false); // if the player remains idle
        } else
        {
            animator.SetBool("Isrunning", true); // if the player is running
        }

        animator.SetFloat("xDir", movement.x); // Set the direction of running
        animator.SetFloat("yDir", movement.y);




        //if(movement.x == 0  && movement.y == 0)
        //{
        //    animator.SetBool(isrunning, false);
        //} else
        //{
        //    animator.SetBool(isrunning, true);
        //}

            //if(movement.x > 0)
            //{
            //    animator.SetInteger(animationState, (int)CharStates.walkEast);
            //}
            //else if (movement.x < 0)
            //{
            //    animator.SetInteger(animationState, (int)CharStates.walkWest);
            //}
            //else if (movement.y > 0)
            //{
            //    animator.SetInteger(animationState, (int)CharStates.walkNorth);
            //}
            //else if (movement.y < 0)
            //{
            //    animator.SetInteger(animationState, (int)CharStates.walkSouth);
            //}

    }
}
