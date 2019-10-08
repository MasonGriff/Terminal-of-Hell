using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //====  References  ====
    public GameObject GMreference;
    GameMaster GM;
    public GameObject Player;

    //====  Camera Spots  ====
    GameObject MainCamera;
    public bool camSet;

    //====  Lock Booleans  ====
    public bool CamLockOff = false;

    // Use this for initialization
    void Start () {
        GMreference = GameObject.Find("GM"); //automatically sets the GM Game Object variable to the object named GM
        GM = GMreference.GetComponent<GameMaster>();
        Player = GameObject.Find("Player");
        MainCamera = this.gameObject;
        camSet = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (CamLockOff == false)
        {//sets 
         /*if (camSet == false) //activates upon entering a different room. Door Controller's script activates it.
         {*/
         // camSet = true;
            Vector3 position = transform.position;
            position.y = GM.CamStart.transform.position.y;
            transform.position = position; //Moves camera to the new room
                                           //}

            if (GM.CamStart.transform.position == GM.CamEnd.transform.position)
            {
                Vector3 stillcam = transform.position;
                stillcam = GM.CamStart.transform.position;
                transform.position = stillcam;
            }

            if (Player.transform.position.x >= GM.CamStart.transform.position.x && Player.transform.position.x <= GM.CamEnd.transform.position.x) //if player is not near the edges of the room.
            {
                Vector3 positionToPlayer = transform.position;
                positionToPlayer.x = Player.transform.position.x;
                transform.position = positionToPlayer; //camera tracks the player
            }

            else if (Player.transform.position.x < GM.CamStart.transform.position.x)
            {
                Vector3 positionToStartEnd = transform.position;
                positionToStartEnd.x = GM.CamStart.transform.position.x;
                transform.position = positionToStartEnd;
            }

            else if (Player.transform.position.x > GM.CamEnd.transform.position.x)
            {
                Vector3 positionToEndEnd = transform.position;
                positionToEndEnd.x = GM.CamEnd.transform.position.x;
                transform.position = positionToEndEnd;
            }
        }
    }
}
