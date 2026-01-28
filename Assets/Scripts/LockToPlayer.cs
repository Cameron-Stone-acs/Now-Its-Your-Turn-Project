using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToPlayer : MonoBehaviour
{
    public GameObject player;
    void LateUpdate()
    {
        this.transform.position = player.transform.position;
    }
}
