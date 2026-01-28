using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) isGrounded = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player")) isGrounded = false;
    }
}
