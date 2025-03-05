using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]

public class Wander : MonoBehaviour
{
    public float pursuitSpeed; // The speed to pursuit the player
    public float wanderSpeed; // The speed to wander
    float currentSpeed; // The current speed

    public float directionChangeInteval; // The Inteval to change direction

    public bool followPlayer;

    Coroutine moveCoroutine;

    Rigidbody2D rb2d;
    Animator animator;

    Transform targetTransform = null;

    Vector3 endPosition; // The target of the player to the position

    float currentAngle = 0;

    public float minX = 30f; // X minimum
    public float maxX = 40f; // X maximum
    public float minY = 23f; // Y minimum
    public float maxY = 31f; // Y maximum

    CircleCollider2D circleCollider;

    [SerializeField] MusicManager musicManager; // Music manager to play the music of pursuiting



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        currentSpeed = wanderSpeed;

        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(WanderRoutine());

        circleCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rb2d.position, endPosition, Color.red);

        //if (targetTransform != null) // If the enemy sees the player
        //{
        //    musicManager.PlayBattleMusic();
        //}
        //else
        //{
        //    musicManager.StopBattleMusic();
        //}

    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();

            if(moveCoroutine != null)
            {

                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

            yield return new WaitForSeconds(directionChangeInteval);
        }
    }

    public void Move()
    {
        
    }

    void ChooseNewEndpoint()
    {

        // Generate random X and Y
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        // Set endPosition as end point
        endPosition = new Vector3(randomX, randomY, 0); // Z stays for 0
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);

    }

    public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;
        //Debug.Log(this.gameObject + "distance: " + remainingDistance);

        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rigidbodyToMove != null)
            {
                animator.SetBool("isWalking", true);


                //Set the original position to be Vector3
                Vector3 originalPosition = new Vector3(rigidbodyToMove.position.x, rigidbodyToMove.position.y, 0);

                // Calculate direction
                Vector3 direction = (endPosition - originalPosition).normalized;

                // Update animator parameters
                animator.SetFloat("xDir", direction.x);
                animator.SetFloat("yDir", direction.y);

                Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.deltaTime);

                rb2d.MovePosition(newPosition);

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
                //Debug.Log(this.gameObject + "distance: " + remainingDistance);

            }

            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("isWalking", false); // If the player is idle
        //Debug.Log(this.gameObject + "is idle");

    }


    private void OnTriggerEnter2D(Collider2D collision) // If the enemy detects the player
    {

        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed; // change the current speed to pursuit speed

            targetTransform = collision.gameObject.transform; // Change to target to be the game Object

            if(moveCoroutine != null) // Stop the current movement if there is one
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed)); // Move to the target

            musicManager.PlayBattleMusic(); // Start playing battle music here
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // If the player run away from with the enemy
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            animator.SetBool("isWalking", false); // Stop walking

            currentSpeed = wanderSpeed; // change the current speed to wander speed


            if (moveCoroutine != null) // Stop the current movement if there is one
            {
                StopCoroutine(moveCoroutine);
            }

            targetTransform = null; // Set the target to null (stop chasing)

            musicManager.StopBattleMusic(); // Start playing battle music here
        }
    }

    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

}
