using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController Controller;
    public float Speed;
    public float jumpHeight = 10f;
    public float boostMult = 2f;
    public float boostTime = 10f;
    public float boostRechargeRate = 0.1f;
    public float boostDischargeRate = 2f;
    public Transform Cam;
    public Rigidbody rb;
    public GameObject gdobject;
    private GroundCheck gd;
    private float boostcharge = 0;
    private bool Delay = false;
    
    void Start()
    {
        gd = gdobject.GetComponent<GroundCheck>();
        boostcharge = boostTime;
    }
    void Update()
    {
        float Horizontal;
        float Vertical;
        
        if (Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f) > 145) Camera.main.fieldOfView = 145;
        else Camera.main.fieldOfView = Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f);
        if (boostcharge <= 0) Delay = true;
        if (boostcharge >= boostTime && Delay) Delay = false;
        if (Input.GetAxis("Boost") > 0 && boostcharge > 0 & gd.isGrounded && !Delay)
        {
            Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime * boostMult;
            Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime * boostTime;
            boostcharge -= Time.deltaTime * boostDischargeRate;
        }
        else 
        {
            Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            if (boostcharge < boostTime) boostcharge += Time.deltaTime * boostRechargeRate;
        }
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
        if (gd.isGrounded && Input.GetAxis("Jump") > 0)
        {
            Movement.y = jumpHeight;
        } 
        rb.AddForce(Movement);
    }
}
