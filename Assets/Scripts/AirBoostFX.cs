using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBoostFX : MonoBehaviour
{
    private CameraController camControl;
    public ParticleSystem FX;
    void Start()
    {
        camControl = GameObject.Find("Air Boost Pivot").GetComponent<CameraController>();
        StartCoroutine(Effect());
    }
    IEnumerator Effect()
    {
        transform.localRotation = Quaternion.Euler(camControl.camRotation.x, camControl.camRotation.y, camControl.camRotation.z);
        FX.Play(false);
        yield return new WaitForSeconds(5);
        FX.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        Destroy(gameObject);
    }
}
