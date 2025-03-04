using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;

    public float spawnInterval = 2f; // Time between spawns in seconds
    private float timer = 0f;

    private Boss boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment timer

        if (timer >= spawnInterval)
        {
            if (boss.isAttack == true)
            {
                // Get Boss's direction.
                Vector3 velocity = boss.GetComponent<Rigidbody2D>().velocity;
                Vector3 moveDirection = velocity.normalized;

                // Instantiate weapon and set it's direction.
                GameObject weapon = Instantiate(weaponPrefab);
                Fireball fireball = weapon.GetComponent<Fireball>();
                fireball.SetDirection(moveDirection);
            }    
            timer = 0f; // Reset timer
        }
    }
}
