using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class doorSwitch : MonoBehaviour {

    //====  Game Control  ====
    public GameObject GMreference; //references the game object holding the Game Master script
    GameMaster GM; //the script which controlls universal functions of the game

    //====  player based stuff  ====
    public bool plyrInLine;

    //====  camera stuff  ====
    public GameObject cameraMain;
    CameraController camCon;
    public GameObject newCamSpot;
    Vector3 cameraJump;
    Vector3 camOldSpot;

    //====  Switch Console  ====
    public bool switchOn = false;
    bool switchDone = false;
    public GameObject offSw;
    public GameObject onSw;
    //float timeKeep;

    //====  Affected Door/Wall  ====
    public GameObject door;
    public float doorMove;
    public GameObject doorMoveObj;

    //====  Sound  ====
    public AudioClip DoorInDistance;
    public AudioSource thisSound;

    void Start()
    {
        cameraMain = GameObject.Find("Main Camera");
        camCon = cameraMain.GetComponent<CameraController>();
        GMreference = GameObject.Find("GM"); //automatically sets the GM Game Object variable to the object named GM
        GM = GMreference.GetComponent<GameMaster>();
        cameraJump = newCamSpot.transform.position;
        thisSound = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        //timeKeep += Time.deltaTime;
        if (switchDone == false)
        {
            if (plyrInLine == true)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                {
                    camOldSpot = cameraMain.transform.position;
                    switchOn = true;
                    offSw.SetActive(false);
                    onSw.SetActive(true);
                    //timeKeep = 0f;
                    plyrInLine = false;
                    thisSound.clip = DoorInDistance;
                    thisSound.PlayDelayed(1);
                }
            }

            if (switchOn == true)
            {
                plyrInLine = false;
                //camCon.CamLockOff = true;
                //GM.overallPaused = true;
                //cameraMain.transform.position = cameraJump;
                //door.transform.DOMoveY(doorMove, 1);
                //timeKeep += Time.deltaTime;

                switchDone = true;
            }
            /*if (door.transform.position.y == doorMove || Input.anyKeyDown && Input.GetKeyUp(KeyCode.Escape))
            {
                cameraMain.transform.position = camOldSpot;
                camCon.CamLockOff = false;
                GM.overallPaused = false;
                switchOn = false;
                switchDone = true;
                Debug.Log("back to cam");
             }*/
        }

        else if (switchDone == true)
        {
            Vector3 doorspot = door.transform.position;
            doorspot.y = doorMoveObj.transform.position.y;
            door.transform.position = doorspot;
        }
	}
}
