using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {
    public string wasteType;
    public PlayerMovement playerScript;

    // Use this for initialization
    void Start() {
        wasteType = "Trash";
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.name.StartsWith("Player") && playerScript.isHolding(wasteType)) {
            playerScript.scoreValue++;
            playerScript.wasteType = "";
            playerScript.setScoreText();
        }
    }
}