using UnityEngine;
using System.Collections;

public class SpawnTrash : MonoBehaviour {
    // Load Prefabs into these arrays
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

    // Player Movement
    public PlayerMovement pm;

    // Spawn Control
    public float frequency;
    public float delay;

    private GameObject[] bins;

    // Initialization
    // Loads all prefabs for later random selection
    // pickups: 0 = trash, 1 = recycling, 2 = compost, 3 = toxic
    void Start () {
        // PlayerMovement
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        // bins
        bins = GameObject.FindGameObjectsWithTag("Bin");
        // Initialize all trash pickups
        pickups = new GameObject[4][];
        pickups[0] = trash;
        pickups[1] = recycling;
        pickups[2] = compost;
        pickups[3] = toxic;

        // Spawn trash after "delay" seconds, every "frequency" seconds
        InvokeRepeating("Spawn", delay, frequency);
    }

    // Spawn one piece of trash
    void Spawn() {
        //// Initialized to fail beginning while loop
        //float x = 12.5f;
        //float y = -5.75f;

        //// Makes sure pickup spawns in area outside of bins
        //while (x >= 1.5 && y <= -4) {
        //    //        while (((x < binCompost.transform.position.x) && (x > binCompost.transform.position.x + 1.5)
        //    //            && (y < binCompost.transform.position.y) && (y > binCompost.transform.position.y + 1.5))
        //    //            || ((x < binTrash.transform.position.x) && (x > binTrash.transform.position.x + 1.5)
        //    //            && (y < binTrash.transform.position.y) && (y > binTrash.transform.position.y + 1.5))
        //    //            || ((x < binRecycling.transform.position.x) && (x > binRecycling.transform.position.x + 1.5)
        //    //            && (y < binRecycling.transform.position.y) && (y > binRecycling.transform.position.y + 1.5))
        //    //            || ((x < binToxic.transform.position.x) && (x > binToxic.transform.position.x + 1.5)
        //    //            && (y < binToxic.transform.position.y) && (y > binToxic.transform.position.y + 1.5))) { 
        //    // x position between left & right border
        //    x = (float)Random.Range(leftBorder.position.x,
        //            rightBorder.position.x) * .9f;

        //    // y position between top & bottom border
        //    y = (float)Random.Range(bottomBorder.position.y,
        //            topBorder.position.y) * .9f;
        //}

        // cancel repeating when out of time
        if (pm.timerValue < 0) {
            CancelInvoke();
        }

        // Chooses a random category to find item to spawn
        int randomCategory = (int)Random.Range(0.0f, 4.0f);

        // Chooses a random pickup from chosen category
        int randomPickup = (int) Random.Range(0.0f, pickups[randomCategory].Length);

        Vector2 position = Vector2.zero;
        int overlap = 0;
        while(overlap < bins.Length) {
            overlap = 0;
            float x = (float)Random.Range(leftBorder.position.x,
                    rightBorder.position.x) * .9f;
            float y = (float)Random.Range(bottomBorder.position.y,
                    topBorder.position.y) * .9f;
            position = new Vector2(x, y);
            // check if 
            for(int i = 0; i < bins.Length; i++) {
                if (Vector2.Distance(bins[i].transform.position, position) >= 0) {
                    overlap++;
                }
            }
        }

        // Instantiate the pickup at (x, y)
        Instantiate(pickups[randomCategory][randomPickup], position,
                    Quaternion.identity);
    }

}