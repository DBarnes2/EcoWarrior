using UnityEngine;
using System.Collections;

public class SpawnTrash : MonoBehaviour {
    // Load Prefabs
    public GameObject[] compost;
    public GameObject[] trash;
    public GameObject[] recycling;
    public GameObject[] toxic;

    // Arrays to hold objects
    public GameObject[][] pickups;

    // Borders
    public Transform topBorder;
    public Transform bottomBorder;
    public Transform leftBorder;
    public Transform rightBorder;

	// Initialization
    // Loads all prefabs for later random selection
    // pickups: 0 = trash, 1 = recycling, 2 = compost, 3 = toxic
	void Start () {
        // Initialize all trash pickups
        pickups = new GameObject[4][];
        pickups[0] = trash;
        pickups[1] = recycling;
        pickups[2] = compost;
        pickups[3] = toxic;

        // Spawn trash after 1 second, every 3 seconds
        InvokeRepeating("Spawn", 0, 3);
    }

    // Spawn one piece of trash
    void Spawn() {
        // Initialized to fail beginning while loop
        float x = 12.5f;
        float y = -5.75f;

        // Makes sure pickup spawns in area outside of bins
        while (x >= 1.5 && y <= -4) {
            // x position between left & right border
            x = (float)Random.Range(leftBorder.position.x,
                    rightBorder.position.x) * .9f;

            // y position between top & bottom border
            y = (float)Random.Range(bottomBorder.position.y,
                    topBorder.position.y) * .9f;
        }

        int randomCategory = Random.Range(0, pickups.Length);
        int randomPickup = Random.Range(0, pickups[randomCategory].Length);

        // Instantiate the pickup at (x, y)
        Instantiate(pickups[randomCategory][randomPickup], new Vector2(x, y),
                    Quaternion.identity);
    }

}
