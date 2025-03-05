using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour // We can damage the enemy
{

    public int damageInflicted;
    public float lifetime = 2.0f; // Time before the ammo disappears if it doesn't hit anything

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));
                gameObject.SetActive(false);
            }

            else
            {
                StartCoroutine(DeactivateAfterTime(lifetime));
            }

            //gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);
        // Deactivate the ammo
        gameObject.SetActive(false);
    }
}
