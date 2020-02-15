using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] pickups;
    private int currPickup = 0;
    //private int ammo = 0;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        // Change ammo type
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currPickup++;
            if (currPickup >= pickups.Length) {
                currPickup = 0;
            }
        }

        // Fire ammo
        if (Input.GetKeyDown(KeyCode.UpArrow) && timeLeft < 0) {
            Instantiate(pickups[currPickup], transform.position, Quaternion.identity);
            timeLeft = 0.5f;
        }
    }
}
