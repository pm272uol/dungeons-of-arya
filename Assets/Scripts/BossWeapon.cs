using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public float spawnInterval = 2f; // Time between spawns in seconds
    public Transform firePoint; // not needed if using boss directly.
    private float timer = 0f;
    private Boss boss;

    public enum FacingDirection { Up, Down, Left, Right }
    public FacingDirection currentFacingDirection = FacingDirection.Down;

    public void UpdateFacingDirection(Vector2 moveInput)
    {
        if (moveInput.y > 0.9)
            currentFacingDirection = FacingDirection.Up;
        else if (moveInput.y < -0.9)
            currentFacingDirection = FacingDirection.Down;
        else if (moveInput.x > 0.9)
            currentFacingDirection = FacingDirection.Right;
        else if (moveInput.x < -0.9)
            currentFacingDirection = FacingDirection.Left;
    }


    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
    }

    public void Fire()
    {
        // Instantiate fireball at firePoint (or enemy position if you don't use firePoint)
        GameObject weapon = Instantiate(weaponPrefab, boss.transform.position, Quaternion.identity);
        Fireball fireball = weapon.GetComponent<Fireball>();
        Animator fireballAnimator = fireball.GetComponent<Animator>();

        Vector2 fireDirection = Vector2.zero;

        switch (currentFacingDirection)
        {
            case FacingDirection.Up:    
                fireDirection = Vector2.up; 
                fireballAnimator.SetFloat("xDir", 0);
                fireballAnimator.SetFloat("yDir", 1);
                break;
            case FacingDirection.Down:  
                fireDirection = Vector2.down; 
                fireballAnimator.SetFloat("xDir", 0);
                fireballAnimator.SetFloat("yDir", -1);
                break;
            case FacingDirection.Left:  
                fireDirection = Vector2.left; 
                fireballAnimator.SetFloat("xDir", -1);
                fireballAnimator.SetFloat("yDir", 0);
                break;
            case FacingDirection.Right: 
                fireDirection = Vector2.right; 
                fireballAnimator.SetFloat("xDir", 1);
                fireballAnimator.SetFloat("yDir", 0);
                break;
        }

        // Set fireball's direction
        fireball.SetDirection(fireDirection);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment timer

        if (timer >= spawnInterval)
        {
            if (boss.isAttack == true)
            {
                Fire();
            }    
            timer = 0f; // Reset timer
        }

        Animator animator = boss.GetComponent<Animator>();
        float x = animator.GetFloat("xDir");
        float y = animator.GetFloat("yDir");
        Vector2 moveInput = new Vector2(x, y);
        UpdateFacingDirection(moveInput);
    }
}
