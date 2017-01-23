using UnityEngine;
using System.Collections;

public class SpawnTrash : MonoBehaviour {
    // Trash Prefab
    public GameObject PickupTrashPrefab;
    public GameObject PickupRecyclingPrefab;

    // Number of Pickups
    public int numPickups;

    // Borders
    public Transform TopBorder;
    public Transform BottomBorder;
    public Transform LeftBorder;
    public Transform RightBorder;

	// Use this for initialization
	void Start () {
        // Spawn trash after 1 second, every 4 seconds
        InvokeRepeating("Spawn", 1, 4);
        numPickups = 2;
    }

    // Spawn one piece of trash
    void Spawn() {
        // x position between left & right border
        int x = (int)Random.Range(LeftBorder.position.x,
                RightBorder.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(BottomBorder.position.y,
                TopBorder.position.y);

        int randomInt = Random.Range(0, numPickups);
        // Instantiate the pickup at (x, y)
        if (randomInt == 0) {
            Instantiate(PickupTrashPrefab, new Vector2(x, y),
                    Quaternion.identity);
        } else if (randomInt == 1) {
            Instantiate(PickupRecyclingPrefab, new Vector2(x, y),
                    Quaternion.identity);
        }
    }

}
