using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    Animator anim;
    PlayerMovement pm;
	// Initialization
	void Awake () {
        anim = GetComponent<Animator>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {
        if (pm.timerValue <= 0) {
            anim.SetTrigger("GameOverTrigger");
        }
    }
}
