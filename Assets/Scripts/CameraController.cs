using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothingFactor = 1;
    public float lookUpMax = 60;
    public float lookUpMin = -60;
    public Transform camera;

    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 offset;

    void Start()
    {
        camRotation = camera.localRotation;
        offset = camera.localPosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * smoothingFactor * -1;
        camRotation.y += Input.GetAxis("Mouse X") * smoothingFactor;
        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMax, lookUpMin);
        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        if (Physics.Linecast(transform.position, transform.position + transform.localRotation * offset, out hit))
        {
            camera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
        }
        else camera.localPosition = Vector3.Lerp(camera.localPosition, offset, Time.deltaTime);
    }
}
