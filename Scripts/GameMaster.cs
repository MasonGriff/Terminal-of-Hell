using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    //====  Gameplay Variables  ====
    public bool overallPaused = false; //all active things such as character movement or AI action require this boolean to be false
    public bool pauseMenuOpen = false;
    public bool ViewingMessage;
    public GameObject player;
    PlayerController playerCon;
    public GameObject overallGameMasterObject; //oject loaded in from title scene
    MasterGameMaster TrueGM; //Master script that keeps record of things outside of the scene. Set to DontDestroyOnLoad and handles what level we're in, the volume levels, and other such things we may implement.
    public bool gotkey1 = false;
    public bool gotkey2 = false;
    public bool gotkey3 = false;
    public bool gotMap = false;
    public bool gotLight = false;
    public bool gotHammer = false;

    //====  HUD Management  ====
    public GameObject HUD;
    GameObject heartsOverview;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject MessageWindow;
    public GameObject pauseMenu;
    int pausetimer = 0;
    public GameObject key1Obj;
    public GameObject key2Obj;
    public GameObject key3Obj; 
    public GameObject minimapDisplay;
    public GameObject minimapText;
    //public GameObject hammerGot;
    public GameObject lightOff;
    public GameObject LightIcon;

    //====  Location Variables/Switches  ====
    public GameObject currentRoom;
    public GameObject endRoom;

    //Current Lane Markers
    public GameObject lane1;
    public GameObject lane2;
    public GameObject lane3;

    //====  Camera Points  ====
    public GameObject CamStart; //The furthest left that the camera can go in this room
    public GameObject CamEnd; //The furthest right that a camera can go in this room



    // Use this for initialization
    void Start () {
        overallPaused = false;
        currentRoom = GameObject.Find("Overview: Starting Room");
        player = GameObject.Find("Player");
        playerCon = player.GetComponent<PlayerController>();
        CamStart = currentRoom.transform.Find("CamStart").gameObject;
        CamEnd = currentRoom.transform.Find("CamEnd").gameObject;
        ViewingMessage = false;
        MessageWindow = HUD.transform.Find("MessagePopup").gameObject;
        MessageWindow.SetActive(false);
        overallGameMasterObject = GameObject.Find("MasterScript");
        TrueGM = overallGameMasterObject.GetComponent<MasterGameMaster>();
        //pauseMenu = GameObject.Find("Pause");
    }
	
	// Update is called once per frame
	void Update () {
        pausetimer = 0;
        lane1 = currentRoom.transform.Find("Lane1").gameObject;
        lane2 = currentRoom.transform.Find("Lane2").gameObject;
        lane3 = currentRoom.transform.Find("Lane3").gameObject;

        CamStart = currentRoom.transform.Find("CamStart").gameObject;
        CamEnd = currentRoom.transform.Find("CamEnd").gameObject;

        if (gotLight == true)
        {
            TrueGM.gotLightMaster = true;
        }
        
        if (TrueGM.gotLightMaster == true)
        {
            gotLight = true;
            Debug.Log("light on");
            lightOff.SetActive(false);
            LightIcon.SetActive(true);
        }

        if (gotLight == false)
        {
            LightIcon.SetActive(false);
        }

        if (gotMap == true)
        {
            minimapDisplay.SetActive(true);
            minimapText.SetActive(true);
        }
    
        //display keys in inventory
        if (gotkey1 == false)
        {
            key1Obj.SetActive(false);
        }
        else if (gotkey1 == true)
        {
            key1Obj.SetActive(true);
        }
        if (gotkey2 == false)
        {
            key2Obj.SetActive(false);
        }
        else if (gotkey2 == true)
        {
            key2Obj.SetActive(true);
        }
        if (gotkey3 == false)
        {
            key3Obj.SetActive(false);
        }
        else if (gotkey3 == true)
        {
            key3Obj.SetActive(true);
        }

        //====  Pause  ====
        if (pauseMenuOpen == true && Input.GetKeyDown(KeyCode.Escape) && pausetimer == 0)
        {
            if (ViewingMessage == false)
            {
                overallPaused = false;
            }
            pauseMenuOpen = false;
            pauseMenu.SetActive(false);
            Debug.Log("unpause");
            pausetimer = 1;
        }



        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuOpen == false && pausetimer == 0)
        {
            overallPaused = true;
            pauseMenuOpen = true;
            pauseMenu.SetActive(true);
            Debug.Log("paused");
            pausetimer = 1;
        }
        
        

        if (ViewingMessage == true)
        {
            MessageWindow.SetActive(true);
            if (pauseMenuOpen == false)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                {
                    ViewingMessage = false;
                    overallPaused = false;
                    MessageWindow.SetActive(false);
                }
            }
        }

        
        if(currentRoom == endRoom)
        {
            SceneManager.LoadScene("TemporaryWin");
        }        

        if (playerCon.Health >= 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }

        if (playerCon.Health == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        if (playerCon.Health == 0)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);

        }
        if (playerCon.Health < 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);

            TrueGM.DeathCount++;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //resets level. Comment this out once a proper Game Over has been implemented.
            SceneManager.LoadScene("GameOver"); //uncomment this when gameover has been properly implemented.
            //SceneManager.UnloadSceneAsync("_PlayerMovementTesting0");
        }
    }
}
