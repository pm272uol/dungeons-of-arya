using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChanger : MonoBehaviour
{
    public Tilemap tilemap;  // Assign your Tilemap in Inspector
    public TileBase newTile; // Assign the new tile in Inspector

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check if the colliding object is the player
        {
            // Get collision position
            Vector3 collisionPoint = collision.contacts[0].point;
            
            // Convert world position to tilemap grid position
            Vector3Int tilePosition = tilemap.WorldToCell(collisionPoint);

            // Change the tile at the collision position
            tilemap.SetTile(tilePosition, newTile);
        }
    }
}