using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    Animator anim;

	// Initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().timerValue <= 0) {
            anim.SetTrigger("GameOverTrigger");
        }
    }
}
