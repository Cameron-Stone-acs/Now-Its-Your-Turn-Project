using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController Controller;
    public float Speed;
    public float jumpHeight = 10f;
    public float boostMult = 2f;
    public float launchMult = 10f;
    public float launchMinSpeed = 0.5f;
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
    private bool launched = false;
    public float hVelocity;
    public float vVelocity;
    void Start()
    {
        gd = gdobject.GetComponent<GroundCheck>();
        boostCharge = boostTime;
    }
    void LateUpdate()
    {
        hVelocity = Mathf.Abs(rb.velocity.x + rb.velocity.z);
        vVelocity = Mathf.Abs(rb.velocity.y);
        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.fixedDeltaTime;
        //fix launch boost when going in a negative direction
        if (Input.GetAxis("Boost") <= 0 && gd.isGrounded && launched && hVelocity < launchMinSpeed) launched = false;
        Debug.Log("Horizontal Velocity: " + hVelocity);
        if (airBoostUsed && gd.isGrounded) airBoostUsed = false;
        if (Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f) > 145) Camera.main.fieldOfView = 145;
        else Camera.main.fieldOfView = Mathf.Lerp(90, rb.velocity.magnitude * 10, 0.1f);
        if (boostCharge <= 0) delay = true;
        if (boostCharge >= boostTime && delay) delay = false;
        if (Input.GetAxis("Boost") > 0 && boostCharge > 0 && gd.isGrounded && !delay && hVelocity > launchMinSpeed)
        {
            Horizontal = Horizontal * boostMult;
            Vertical = Vertical * boostMult;
            boostCharge -= Time.fixedDeltaTime * boostDischargeRate;
        }
        else if (Input.GetAxis("Boost") > 0 && hVelocity < launchMinSpeed && gd.isGrounded && !delay && !launched && boostCharge >= boostTime/10)
        {
            Vertical = 1 * Speed * Time.fixedDeltaTime * launchMult;
            Debug.Log("Launch");
            boostCharge -= boostTime/10;
            launched = true;
        }
        else if (boostCharge < boostTime) boostCharge += Time.fixedDeltaTime * boostRechargeRate;
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
        if (Input.GetAxis("Boost") > 0 && Input.GetAxis("Jump") <= 0 && !gd.isGrounded && boostCharge >= boostTime / 2 && !airBoostUsed && !delay)
        {
            boostCharge -= boostTime/2;
            Vertical = 1 * Speed * Time.fixedDeltaTime * airBoostMult;
            Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            airBoostUsed = true;
        }
        if (gd.isGrounded && Input.GetAxis("Jump") > 0) Movement.y = jumpHeight;
        rb.AddForce(Movement);
    }
}
