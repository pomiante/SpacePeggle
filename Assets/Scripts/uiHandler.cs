using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class uiHandler : MonoBehaviour
{
    TextMeshProUGUI text;
    playerShip player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerShip>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "HP: " + player.healthPoints + "\nBalls: " + player.Balls;
    }
}
