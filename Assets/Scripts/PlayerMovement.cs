using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D player;

    // Player's score par to achieve
    public int par;

    //Type of waste holding
    public string wasteType;

    // Movement Keys (customizable)
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    // Movement
    public float speed;
    Vector2 direction;

    // Score Text
    public Text scoreText;
    public int scoreValue;

    // Timer Text
    public Text timerText;
    public float timerValue;

    // Game Over Message
    public Text gameOverText;

    // Help Text and NPC
    public Text helpText;
    public GameObject helpNPC;

    // Continue to update?
    public bool update;

    // Object that follows
    public GameObject follow;

    // Initializes game
    public void Start() {
        setScoreText();
        setTimerText();

        player = this.GetComponent<Rigidbody2D>();
        direction = Vector2.up * speed;
        wasteType = "";
        update = true;
    }

    // Once per frame
    public void Update() {
        if (update) {
            // movement
            if (Input.GetKey(upKey)) {
                direction = Vector2.up * speed;
            } else if (Input.GetKey(downKey)) {
                direction = -Vector2.up * speed;
            } else if (Input.GetKey(rightKey)) {
                direction = Vector2.right * speed;
            } else if (Input.GetKey(leftKey)) {
                direction = -Vector2.right * speed;
            }
            player.velocity = direction;
            // change timer, check if game is over
            if (timerValue > 0) {
                timerValue -= Time.deltaTime;
            } else {
                this.gameOver();
            }
            setTimerText();
            setScoreText();

            // link help text position with help NPC
            //Vector2 helpTextPosition = helpNPC.transform.position;
            //helpTextPosition.y += 50;
            //helpText.transform.position = helpTextPosition;

            follow.transform.position = player.transform.position;
        }
    }

    // Activates when player collides with a trigger
    // TODO: Generalize with arrays
    public void OnTriggerEnter2D(Collider2D coll) {
        if (!this.isHoldingAnything() && coll.name.StartsWith("Pickup")) {
            this.wasteType = coll.tag;
            follow = coll.gameObject;
        } else if (coll.name.Equals("HelpNPC")) {
            if (this.isHolding("Toxic")) {
                helpText.text = "\"That's e-waste!\"";
            } else if (this.isHoldingAnything()) {
                helpText.text = "\"That's " + wasteType + "!\"";
            } else {
                helpText.text = "\"You're not holding anything!\"";
            }
        }
    }

    // Sets the score text to the current score
    public void setScoreText () {
        scoreText.text = "Score: " + scoreValue.ToString();
        //if (timerValue < 0) {
        //    if (scoreValue < 5) {
        //        scoreText.text += "You can do better! Learn your sorting better!";
        //    } else {
        //        scoreText.text += "Nice job!";
                  // Thanks for helping UW reach our sustainability goal of 70% by 2020!
        //    }
        //}
    }

    // Sets the timer text to the current time
    public void setTimerText() {
        timerText.text = "Timer: " + Mathf.RoundToInt(timerValue).ToString();
    }

    //Checks if player is holding a certain type of waste
    public bool isHolding(string wasteType) {
        return this.wasteType.Equals(wasteType);
    }

    //Checks if player is holding any type of waste
    public bool isHoldingAnything() {
        return !this.isHolding("");
    }

    // Stops the player character and stops the game from updating
    public void gameOver() {
        player.velocity = Vector2.zero;
        update = false;
        helpText.text = "";
        if (scoreValue < par) {
            gameOverText.text = "Par was " + par + ". You can do better! Remember, accuracy is more important than speed!";
        } else {
            gameOverText.text = "Par was " + par + ". Great job! You are helping UW in its sustainability goal of reaching 70% waste diversion by 2020!";
        }
    }
}