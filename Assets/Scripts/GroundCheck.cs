using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public GameObject Player;
    // Update is called once per frame
    void Update()
    {
        Transform.position(new Vector3(Player.transform.x, Player.transform.y - 0.5, Player.transform.z));
    }
}
