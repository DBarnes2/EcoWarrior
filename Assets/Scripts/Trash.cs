using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {
    public string wasteType;
    public PlayerMovement playerScript;

    AudioSource positive;
    AudioSource negative;

    // Use this for initialization
    void Start() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        AudioSource[] audio = GetComponents<AudioSource>();
        positive = audio[0];
        negative = audio[1];

    }

    void OnTriggerEnter2D(Collider2D coll) {
        Destroy(playerScript.follow);
        if (coll.name.StartsWith("Player") && playerScript.isHolding(wasteType)) {
            playerScript.scoreValue++;
            playerScript.wasteType = "";
            playerScript.setScoreText();
            positive.Play();
        } else if (coll.name.StartsWith("Player") && playerScript.isHoldingAnything()) {
            playerScript.wasteType = "";
            negative.Play();
        }
    }
}