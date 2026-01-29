using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController Controller;
    public float Speed;
    public float jumpHeight = 10f;
    public float boostMult = 2f;
    public float airBoostMult = 5f;
    public float boostTime = 10f;
    public float boostRechargeRate = 0.1f;
    public float boostDischargeRate = 2f;
    public Transform Cam;
    public Rigidbody rb;
    public GameObject gdobject;
    private GroundCheck gd;
    public float boostCharge = 0;
    private bool delay = false;
    private bool airBoostUsed = false;
    
    void Start()
    {
        gd = gdobject.GetComponent<GroundCheck>();
        boostCharge = boostTime;
    }
    void LateUpdate()
    {
        float Horizontal;
        float Vertical;
        if (airBoostUsed && gd.isGrounded) airBoostUsed = false;
        if (Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f) > 145) Camera.main.fieldOfView = 145;
        else Camera.main.fieldOfView = Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f);
        if (boostCharge <= 0) delay = true;
        if (boostCharge >= boostTime && delay) delay = false;
        if (Input.GetAxis("Boost") > 0 && boostCharge > 0 & gd.isGrounded && !delay)
        {
            Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime * boostMult;
            Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime * boostMult;
            boostCharge -= Time.deltaTime * boostDischargeRate;
        }
        else 
        {
            Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            if (boostCharge < boostTime) boostCharge += Time.deltaTime * boostRechargeRate;
        }
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
        if (Input.GetAxis("Boost") > 0 && Input.GetAxis("Jump") <= 0 && !gd.isGrounded && boostCharge >= boostTime / 2 && !airBoostUsed && !delay)
        {
            boostCharge -= boostTime/2;
            Vertical = 1 * Speed * Time.deltaTime * airBoostMult;
            Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            airBoostUsed = true;
        }
        if (gd.isGrounded && Input.GetAxis("Jump") > 0)
        {
            Movement.y = jumpHeight;
        } 
        rb.AddForce(Movement);
    }
}
