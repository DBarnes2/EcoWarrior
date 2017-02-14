using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    //Type of waste holding
    public string wasteType;

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

    //public bool holdingObject;

    // Initializes game
    void Start() {
        scoreValue = 0;
        setScoreText();
        timerValue = 30;
        setTimerText();
        speed = 6;
        wasteType = "";
    }

    // FixedUpdate is called regularly
    // TODO: make more responsive
    void FixedUpdate() {
        //Move in a new Direction?
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
        if (timerValue > 0) {
            timerValue -= Time.deltaTime;
        }
        setTimerText();
        setScoreText();
    }

    // Activates when player collides with a trigger
    // TODO: Generalize with arrays
    void OnTriggerEnter2D(Collider2D coll) {
        if (!this.isHoldingAnything() && coll.name.StartsWith("Pickup")) {
            this.wasteType = coll.tag;
            Destroy(coll.gameObject);
        }
    }

    // Sets the score text to the current score
    public void setScoreText () {
        scoreText.text = "Score: " + scoreValue.ToString();
    }

    // Sets the timer text to the current time
    void setTimerText() {
        timerText.text = "Timer: " + Mathf.RoundToInt(timerValue).ToString();
    }

    //Checks if player is holding a certain type of waste
    public bool isHolding(string wasteType) {
        return this.wasteType.Equals(wasteType);
    }

    //Checks if player is holding any type of waste
    bool isHoldingAnything() {
        return !this.isHolding("");
    }
}
