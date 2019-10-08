using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{

    //==== Locked Door 1 ====
    public GameObject keyToRoom1; //Assign the key for the first locked door to this GameObject.
    public GameObject lockedDoor1; //Assign the first locked door to this GameObject.

    //==== HUD Elements ====
    public GameObject Key1; //Will automatically be found.

    private bool triggerEntered; //Determines whether player is inside the trigger of a locked door.

    void Start()
    {
        triggerEntered = false;

        Key1 = GameObject.Find("Key1");
    }

    void Update()
    {
        if (triggerEntered == true && keyToRoom1.activeInHierarchy == false && Input.GetKeyDown(KeyCode.E)) //Unlocks respective door ONLY if player is inside trigger, holding correct key (determined if key is disabled), and presses "E".
        {
            lockedDoor1.GetComponent<DoorController>().locked = false;

            Key1.gameObject.SetActive(false); //Hides the Key1 HUD element.
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == lockedDoor1)
        {
            triggerEntered = true;

            Debug.Log("locked door");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == lockedDoor1)
        {
            triggerEntered = false;
        }
    }
}
