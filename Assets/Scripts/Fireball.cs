using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Fireball needs to move itself in the initial direction so that next fireball can be shot in a different direction.

    private Coroutine damageCoroutine;
    public int damage = 1;

    private int direction = -1;
    private string directionId = "yDir";

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = gameObject.GetComponent<Animator>(); 
        animator.SetFloat(directionId, direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (directionId == "xDir")
        {
            if (direction == -1)
            {
                transform.position += new Vector3(-0.1f, 0, 0);
            }
            else if (direction == 1) {
                transform.position += new Vector3(0.1f, 0, 0);
            }
        }
        else if (directionId == "yDir")
        {
            if (direction == -1)
            {
                transform.position -= new Vector3(0, -0.1f, 0);
            }
            else if (direction == 1) 
            {
                transform.position -= new Vector3(0, 0.1f, 0);
            }
        }
    }

    public void SetDirection(Vector3 dir)
    {
        if (dir.x < 0)
        {
            directionId = "xDir";
            direction = -1;
        } 
        else if (dir.x > 0) 
        {
            directionId = "xDir";
            direction = 1;
        } 
        else if (dir.y < 0) 
        {
            directionId = "yDir";
            direction = -1;
        } 
        else if (dir.y > 0) 
        {
            directionId = "yDir";
            direction = 1;
        }   
        Debug.Log(dir);
        Debug.Log(directionId);
        Debug.Log(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fireball collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damage, 0.0f)); // Inflict damage to the player
            }
            Destroy(gameObject);
        } 
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
