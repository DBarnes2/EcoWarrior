using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    // Movement Keys (customizable)
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    // Movement Speed
    public float speed;

    // Score Text
    public Text scoreText;
    public int scoreValue;

    // Timer Text
    public Text timerText;
    public float timerValue;

    // Is player carrying object?
    public bool holdingTrash;
    public bool holdingRecycling;
    public bool holdingCompost;
    public bool holdingObject;

    // Initializes game
    void Start() {
        // Text
        scoreValue = 0;
        setScoreText();
        timerValue = 30;
        setTimerText();

        // initialization
        holdingTrash = false;
        holdingRecycling = false;
        holdingCompost = false;
        holdingObject = false;
        speed = 6;

    }

    // FixedUpdate is called regularly
    // TODO: make more responsive
    void FixedUpdate() {
        // Move in a new Direction?
        if (Input.GetKey(upKey)) {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        } else if (Input.GetKey(downKey)) {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
        } else if (Input.GetKey(rightKey)) {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        } else if (Input.GetKey(leftKey)) {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
        }
    }

    // Once per frame
    void Update() {
        // Change timer
        timerValue -= Time.deltaTime;
        setTimerText();

        // Is game over?
        if (timerValue < 0) {
            gameOver();
        }
    }

    // Activates when player collides with a trigger
    // TODO: Generalize with arrays
    void OnTriggerEnter2D(Collider2D coll) {
        // Is collider object?
        if (!holdingObject && coll.name.StartsWith("Pickup")) {
            // If not already holding trash, "picks up" object,
            // and marks player as holding trash
            // TODO: Redundant?
            if (!holdingTrash && coll.name.StartsWith("PickupTrash")) {
                holdingTrash = true;
                Destroy(coll.gameObject);
            } else if (!holdingRecycling && coll.name.StartsWith("PickupRecycling")) {
                holdingRecycling = true;
                Destroy(coll.gameObject);
            } else if (!holdingCompost && coll.name.StartsWith("PickupCompost")) {
                holdingCompost = true;
                Destroy(coll.gameObject);
            }
            holdingObject = true;
        }

        // Is collider a bin?
        if (holdingObject && coll.name.StartsWith("Bin")) {
            // If holding object, increments score, and
            // marks player as not holding object
            if (holdingTrash && coll.name.StartsWith("BinTrash")) {
                scoreValue = scoreValue + 1;
                holdingTrash = false;
                holdingObject = false;
                setScoreText();
            } else if (holdingRecycling && coll.name.StartsWith("BinRecycling")) {
                scoreValue = scoreValue + 1;
                holdingRecycling = false;
                holdingObject = false;
                setScoreText();
            } else if (holdingCompost && coll.name.StartsWith("BinCompost")) {
                scoreValue = scoreValue + 1;
                holdingCompost = false;
                holdingObject = false;
                setScoreText();
            }
        }
    }

    // Sets the score text to the current score
    void setScoreText () {
        scoreText.text = "Score: " + scoreValue.ToString();
    }

    // Sets the timer text to the current time
    void setTimerText() {
        timerText.text = "Timer: " + Mathf.RoundToInt(timerValue).ToString();
    }

    // Is called if the game is over. 
    // Sets the time to 0 and stops the player
    // TODO: Object melt into ground?
    void gameOver() {
        timerValue = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
