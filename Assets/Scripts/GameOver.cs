using UnityEngine;
using UnityEngine.SceneManagement;
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
            // Restart scene after a delay of 5f
            Invoke("Restart", 5f);
        }
    }

    // Restart Scene
    void Restart() {
        if (SceneManager.GetActiveScene().buildIndex < 2) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            SceneManager.LoadScene(0);
        }
    }
}
