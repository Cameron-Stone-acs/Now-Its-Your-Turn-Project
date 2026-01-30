using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float jumpHeight = 10f;
    public float boostMult = 2f;
    public float launchMult = 10f;
    public float launchMinSpeed = 0.5f;
    public float airBoostMult = 5f;
    public float boostTime = 10f;
    public float boostRechargeRate = 0.1f;
    public float boostDischargeRate = 2f;
    public float slamSpeed = 5;
    public Transform Cam;
    public Rigidbody rb;
    public GroundCheck gd;
    public float boostCharge = 0;
    public bool jumped = false;
    public bool delay = false;
    public bool boosting = false;
    public bool airBoostUsed = false;
    public bool launched = false;
    public bool slaming = false;
    public float boostLastUsed = 1f;
    public float hVelocity;
    public float vVelocity;
    void Start()
    {
        boostCharge = boostTime;
    }
    void FixedUpdate()
    {
        //Velocity calculations
        hVelocity = Mathf.Abs(rb.velocity.x + rb.velocity.z);
        vVelocity = Mathf.Abs(rb.velocity.y);
        //movement input
        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.fixedDeltaTime;
        //boost functions

        if (Input.GetAxis("Boost") <= 0 && gd.isGrounded && launched && hVelocity < launchMinSpeed) launched = false;

        if (airBoostUsed && gd.isGrounded) airBoostUsed = false;

        if (Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f) > 145) Camera.main.fieldOfView = 145;
        else Camera.main.fieldOfView = Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f);

        if (boostCharge <= 0) delay = true;

        if (boostCharge >= boostTime && delay) delay = false;

        if (Input.GetAxis("Boost") > 0 && boostCharge > 0 && gd.isGrounded && !delay && hVelocity > launchMinSpeed)
        {
            boostLastUsed = 0;
            Horizontal = Horizontal * boostMult;
            Vertical = Vertical * boostMult;
            boostCharge -= Time.fixedDeltaTime * boostDischargeRate;
            boosting = true;
            launched = false;
        }
        else if (Input.GetAxis("Boost") > 0 && hVelocity < launchMinSpeed && gd.isGrounded && !delay && !launched && boostCharge >= boostTime/10)
        {
            Vertical = 1 * Speed * Time.fixedDeltaTime * launchMult;
            boostCharge -= boostTime/10;
            launched = true;
            boosting = true;
        }
        else if (boostCharge < boostTime) 
        {
            boostCharge += Time.fixedDeltaTime * boostRechargeRate;
            boostLastUsed += Time.fixedDeltaTime;
        }
        else boostLastUsed += Time.fixedDeltaTime;
        //movement calculations based on camera rotation
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;

        Movement.y = 0f;
        //air boost function
        if (Input.GetAxis("Boost") < 0 && Input.GetAxis("Jump") <= 0 && boostLastUsed > 0 && !gd.isGrounded && boostCharge >= boostTime / 2 && !airBoostUsed && !delay && !boosting)
        {
            boostCharge -= boostTime/2;
            Vertical = 1 * Speed * Time.fixedDeltaTime * airBoostMult;
            Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            airBoostUsed = true;
        }
        //prevents instantly air boosting if you jump while holding boost
        if (Input.GetAxis("Boost") < 1) boosting = false;
        //jump
        if (gd.isGrounded && Input.GetAxis("Jump") > 0)
        {
            Movement.y = jumpHeight;
            jumped = true;
        }
        if (gd.isGrounded && Input.GetAxis("Jump") <= 0) jumped = false;
        //slam function (not functional/work in progress)
        if (Input.GetAxis("Slam") > 0 && Input.GetAxis("Jump") <= 0 && !gd.isGrounded && boostCharge > 0 && !delay)
        {
            Movement.y -= slamSpeed;
            boostCharge -= Time.fixedDeltaTime * boostDischargeRate;
            slaming = true;
        }
        else slaming = false;
        //apply force
        rb.AddForce(Movement);
    }
}
