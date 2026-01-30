using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text boostText;
    //public TMP_Text winText;
    public PlayerController player;
    //public YouWin win;
     void Awake()
    {
        
    }
    void Update()
    {
        boostText.text = Mathf.Round(player.boostCharge).ToString();
        if (player.delay) boostText.color = new Color(255, 0, 0, 255);
        else boostText.color = new Color(255, 255, 255, 255);
        //if (win.youWin) winText.text = "You Win";
    }
}
