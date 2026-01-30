using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip AirBoostSound;
    public float volume;

    public ParticleSystem moveFX;
    public ParticleSystem jumpFX;
    public GameObject AirBoostFX;
    public PlayerController pControl;
    public GroundCheck groundCheck;
    public CameraController camControl;
    private bool AirBoostFXPlayed = false;
    void FixedUpdate()
    {
        if (groundCheck.isGrounded && pControl.hVelocity > 2f) 
        {
            var em = moveFX.emission;
            moveFX.Play(false);
            em.rateOverTime = 80f * pControl.hVelocity * 0.1f;
        }
        else moveFX.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        if (pControl.jumped && groundCheck.isGrounded) jumpFX.Play(false);
        else jumpFX.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        if (!groundCheck.isGrounded && pControl.airBoostUsed && !AirBoostFXPlayed) 
        {
            Instantiate(AirBoostFX, this.transform.position, Quaternion.identity);
            source.PlayOneShot(AirBoostSound, volume);
            AirBoostFXPlayed = true;
        }
        else if (groundCheck.isGrounded) AirBoostFXPlayed = false;
    }
    
}
