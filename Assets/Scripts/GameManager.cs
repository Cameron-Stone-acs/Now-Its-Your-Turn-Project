using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text boostText;
    public PlayerController player;
     void Awake()
    {
        
    }
    void Update()
    {
        boostText.text = Mathf.Round(player.boostCharge).ToString();
    }
}
