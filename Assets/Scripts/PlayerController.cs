using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController Controller;
    public float Speed;
    public float jumpHeight = 10;
    public Transform Cam;
    public Rigidbody rb;
    public GameObject gdobject;
    private GroundCheck gd;
    void Start()
    {
        gd = gdobject.GetComponent<GroundCheck>();
    }
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
        if (gd.isGrounded && Input.GetAxis("Jump") > 0)
        {
            Movement.y = jumpHeight;
        } 
        rb.AddForce(Movement);
    }
}
