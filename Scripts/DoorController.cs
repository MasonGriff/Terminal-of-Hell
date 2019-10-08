using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    //=====  Game Functions  ====
    public GameObject playerObject; //References the object that holds the player controller script
    PlayerController player; //references the script controlling the player
    public GameObject GMreference;
    GameMaster GM;
    public GameObject CameraBody;
    CameraController CameraCon;

    //====  Public Variables  ====
    public int neededLane; //check if player is in the lane needed to use the door
    public bool locked; //switches the door to being loked or unlocked
    public GameObject doorLocked; //game object that's visible when the door is locked
    public GameObject doorUnlocked; //game object that's visible when the door is unlocked
    public GameObject targetToMove;
    public GameObject overviewRoom; //the parent layer that holds all the next room's gameobjects. Keep the position for this empty game object at [0, 0 ,0].
    public bool usingDoorNow = false;
    public bool Key1Needed = false;
    public bool Key2Needed = false;
    public bool Key3Needed = false;

    //====  Private Variables  ====
    bool playerInLane;
    GameObject door; //references the game object holding this script at the moment


	// Use this for initialization
	void Start () {
        playerObject = GameObject.Find("Player"); //automatically sets the player Game Object variable to the object named Player
        GMreference = GameObject.Find("GM"); //automatically sets the GM Game Object variable to the object named GM
        CameraBody = GameObject.Find("Main Camera");
        door = this.gameObject; //sets the door game object variable to the object holding this script
        doorLocked = door.transform.Find("LockedDoor").gameObject; //finds the locked door child game object
        doorUnlocked = door.transform.Find("OpenDoor").gameObject; //finds the unlocked door child game object
        GM = GMreference.GetComponent<GameMaster>(); //can reference things in the overall game master script. Pausing for example
        player = playerObject.GetComponent<PlayerController>(); //connects the variable "player" to the script attached to the player object.
        CameraCon = CameraBody.GetComponent<CameraController>(); //gets camera script off main camera
    }

    // Update is called once per frame
    void Update () {
		if (locked == true) //switches to the locked door game object
        {
            doorLocked.SetActive(true);
            doorUnlocked.SetActive(false);
        }
        if (locked == false) //switches to unlocked door game object
        {
            doorLocked.SetActive(false);
            doorUnlocked.SetActive(true);
        }
        if(usingDoorNow == true)
        {
            UsingDoor();
            usingDoorNow = false;
        }
    }

    void UsingDoor()
    {
        GM.currentRoom = overviewRoom; //changes the game master's reference point for which room the player's in. Lets the Game Master script automatically update the lane locations.
        //teleports player to new position in the next room
        Vector3 playerPos = playerObject.transform.position;
        playerPos.x = targetToMove.transform.position.x;
        playerObject.transform.position = playerPos;


        //teleports camera
        Vector3 positioncam = CameraBody.transform.position;
        positioncam = GM.CamStart.transform.position;
        CameraBody.transform.position = positioncam;

        //CameraCon.camSet = false;
        //NewLaneController();
    }

    /*void NewLaneController() //updates the lane the player's now at in the new room
    {
       /* if (player.plyrLane == 1)
        {
            player.plyrLane = 3;
        }

        if (player.plyrLane == 3)
        {
            player.plyrLane = 1;
        } 
    }*/
}
