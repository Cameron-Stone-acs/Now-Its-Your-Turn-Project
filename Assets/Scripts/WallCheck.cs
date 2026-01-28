using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool wallCollision;
    private void OnTriggerEnter(Collider collision) 
    {
        if (!collision.CompareTag("Player")) wallCollision = true;
    }
    private void OnTriggerExit(Collider collision) 
    {
        if (!collision.CompareTag("Player")) wallCollision = false;
    }
}
