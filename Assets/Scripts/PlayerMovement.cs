using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D rigid;

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

    // Initializes game
    void Start() {
        setScoreText();
        setTimerText();

        rigid = this.GetComponent<Rigidbody2D>();
        direction = Vector2.up * speed;
        wasteType = "";
    }

    // Once per frame
    void Update() {
        // Change timer
        // movement
        // Move in a new Direction?
        if (Input.GetKey(upKey)) {
            direction = Vector2.up * speed;
        } else if (Input.GetKey(downKey)) {
            direction = -Vector2.up * speed;
        } else if (Input.GetKey(rightKey)) {
            direction = Vector2.right * speed;
        } else if (Input.GetKey(leftKey)) {
            direction = -Vector2.right * speed;
        }
        rigid.velocity = direction;
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
