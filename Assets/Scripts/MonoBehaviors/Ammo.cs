using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour // We can damage the enemy
{

    public int damageInflicted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));

            gameObject.SetActive(false);
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
}
