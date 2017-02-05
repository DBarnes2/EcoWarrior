using UnityEngine;
using System.Collections;

public class SpawnTrash : MonoBehaviour {
    // Load Prefabs
    public GameObject PickupCompostUtensilsPrefab;
    public GameObject PickupRecyclingColaPrefab;
    public GameObject PickupToxicBatteryPrefab;
    public GameObject PickupTrashStyrofoamPrefab;
    public GameObject PickupRecylingWaterBottle;

    // Number of Pickups
    public int numPickups;

    // Arrays to hold objects
    public GameObject[][] pickups;

    // Borders
    public Transform TopBorder;
    public Transform BottomBorder;
    public Transform LeftBorder;
    public Transform RightBorder;

	// Initialization
    // Loads all prefabs for later random selection
    // pickups: 0 = trash, 1 = recycling, 2 = compost, 3 = toxic
	void Start () {
        // Spawn trash after 1 second, every 3 seconds
        pickups = new GameObject[4][];
        pickups[0] = new GameObject[1];
        pickups[1] = new GameObject[2];
        pickups[2] = new GameObject[1];
        pickups[3] = new GameObject[1];

        pickups[0][0] = PickupTrashStyrofoamPrefab;
        pickups[1][0] = PickupRecyclingColaPrefab;
        pickups[1][1] = PickupRecylingWaterBottle;
        pickups[2][0] = PickupCompostUtensilsPrefab;
        pickups[3][0] = PickupToxicBatteryPrefab;
        InvokeRepeating("Spawn", 1, 3);
    }

    // Spawn one piece of trash
    void Spawn() {
        // Initialized to fail beginning while loop
        float x = 12.5f;
        float y = -5.75f;
        // Makes sure pickup spawns in area outside of bins
        while (x >= 1.5 && y <= -4) {
            // x position between left & right border
            x = (float)Random.Range(LeftBorder.position.x,
                    RightBorder.position.x);

            // y position between top & bottom border
            y = (float)Random.Range(BottomBorder.position.y,
                    TopBorder.position.y);
        }

        int randomCategory = Random.Range(0, pickups.Length);
        int randomPickup = Random.Range(0, pickups[randomCategory].Length);
        // Instantiate the pickup at (x, y)
        Instantiate(pickups[randomCategory][randomPickup], new Vector2(x, y),
                    Quaternion.identity);
    }

}
