using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycling : MonoBehaviour {
    public string wasteType;
    public PlayerMovement playerScript;

    // Use this for initialization
    void Start() {
        wasteType = "Recycling";
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update() {
        playerScript.setScoreText();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.name.StartsWith("Player") && playerScript.isHolding(wasteType)) {
            playerScript.scoreValue++;
            playerScript.wasteType = "";
            playerScript.setScoreText();
        }
    }
}