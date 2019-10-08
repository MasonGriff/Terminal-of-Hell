using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// This is the script for controlling the player's actions.
    /// Input from the player is controlled here.
    /// Script by: Mason Griff
    /// </summary>


    //====  Game Functions ====
    public GameObject GMreference; //references the game object holding the Game Master script
    GameMaster GM; //the script which controlls universal functions of the game
    public GameObject player;
    // public bool paused;
    DoorController door;
    MessageController Mes;
    doorSwitch swtch;
    public GameObject BreakableWall;
    public bool nearBreakableWall;

    //====  Public Variables  ====
    // movement speed
    public float plyrWalkSpeed;
    public float walkSpeedDefault = 4f;
    public float sprintSpeedDefault = 10f;

    // Movement Time Managers
    public float sprintTime = 5f;
    float sprintCooldown = 0f;
    float sprintCooldownManager = 0f;
    public float sprintCooldownManagerMaximum = 5f;

    // Life
    public int Health = 2;

    // Lane Management
    public int plyrLane;

    //Message Boolean
    public bool checkMessage = false;

    //====  Private Variables ====
    private Rigidbody2D myRB;
    bool facingRight = true; //is the player facing right?
    bool dying = false; //Switch to start death functions
    bool touchingDoor = false;
    bool touchingLockedDoor = false;
    bool inPlace = false;
    bool isRunning = false;

    //====  Spritework  ====
    public GameObject SpriteChild;
    public Animator SpriteAnim;
    public GameObject MiniMapBlip;
    public GameObject pressE;

    //====  Sound  ====
    public AudioClip Crumble;
    public AudioClip LockedSound;
    public AudioSource mySound;


    // Use this for initialization
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySound = GetComponent<AudioSource>();
        GMreference = GameObject.Find("GM");
        plyrWalkSpeed = walkSpeedDefault; //sets the default walk speed upon loading in.
        plyrLane = 2; //starts player on the middle lane
        GM = GMreference.GetComponent<GameMaster>(); //can reference things in the overall game master script. Pausing for example;
        player = this.gameObject;
        SpriteChild = player.transform.Find("PlayerSprite").gameObject;
        SpriteAnim = SpriteChild.GetComponent<Animator>();
        MiniMapBlip.SetActive(true);
        pressE = player.transform.Find("E-Interact").gameObject;
        pressE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //if () aborted if statement


        if (dying == false && GM.overallPaused == false) //these only work if the player isn't dead and the game isn't paused
        {

            sprintCooldown -= Time.deltaTime;
            if (sprintCooldown <= 0) //player's stopped running and/or can't run anymore
            {
                if (isRunning == true)
                {
                    isRunning = false;
                    sprintCooldownManager = sprintCooldownManagerMaximum;
                }
                if (isRunning == false)
                {
                    sprintCooldownManager -= Time.deltaTime;
                }
            }

            if (plyrLane == 1) //top lane
            {
                Vector3 position = transform.position;
                position.y = GM.lane1.transform.position.y;
                position.z = -1.5f;
                //position.z = GM.lane1.transform.position.z;
                transform.position = position;

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    plyrLane = 2;
                    inPlace = false;
                    
                }
            }
            else if (plyrLane == 2) //mid lane
            {
                Vector3 position = transform.position;
                position.y = GM.lane2.transform.position.y;
                position.z = -2f;
                //position.z = GM.lane2.transform.position.z;
                transform.position = position;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    plyrLane = 1;
                    inPlace = false;
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    plyrLane = 3;
                    inPlace = false;
                }
            }
            else if (plyrLane == 3) //bottom lane
            {
                Vector3 position = transform.position;
                position.y = GM.lane3.transform.position.y;
                position.z = -3f;
                //position.z = GM.lane3.transform.position.z;
                transform.position = position;
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    plyrLane = 2;
                    inPlace = false;
                }

            }

            //====  Checking if in Front of Door  ====
            if (touchingDoor == true && inPlace == true)
            {
                if (plyrLane == door.neededLane && door.neededLane == 1)
                {
                    Debug.Log("lanes match");
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                    {
                        door.usingDoorNow = true;
                        //touchingDoor = false;
                        //Debug.Log("Pressed up");
                        plyrLane = 3;
                    }
                }
                if (plyrLane == door.neededLane && door.neededLane == 3)
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        door.usingDoorNow = true;
                        //touchingDoor = false;
                        plyrLane = 1;
                    }
                }
            }

            //in front of locked door
            if (touchingLockedDoor == true && inPlace == true)
            {
                if (plyrLane == door.neededLane /*&& door.neededLane == 1*/)
                {
                    Debug.Log("lanes match");
                    if (Input.GetKeyDown(KeyCode.UpArrow) && door.neededLane == 1 || Input.GetKeyDown(KeyCode.DownArrow) && door.neededLane == 3 || Input.GetKeyDown(KeyCode.W) && door.neededLane == 1 || Input.GetKeyDown(KeyCode.S) && door.neededLane == 3)
                    {
                        if (door.Key1Needed == true)
                        {
                            if (GM.gotkey1 == true)
                            {
                                door.locked = false;
                                touchingLockedDoor = false;
                                touchingDoor = true;
                                Debug.Log("door unlocked now");
                                GM.gotkey1 = false;
                            }
                            else
                            {
                                mySound.clip = LockedSound;
                                mySound.Play();
                            }
                        }
                        if (door.Key2Needed == true)
                        {
                            if (GM.gotkey2 == true)
                            {
                                door.locked = false;
                                touchingLockedDoor = false;
                                touchingDoor = true;
                                Debug.Log("door unlocked now");
                                GM.gotkey2 = false;
                            }
                            else
                            {
                                mySound.clip = LockedSound;
                                mySound.Play();
                            }
                        }
                        if (door.Key3Needed == true)
                        {
                            if (GM.gotkey3 == true)
                            {
                                door.locked = false;
                                touchingLockedDoor = false;
                                touchingDoor = true;
                                Debug.Log("door unlocked now");
                                GM.gotkey3 = false;
                            }
                            else
                            {
                                mySound.clip = LockedSound;
                                mySound.Play();
                            }
                        }
                        
                        //will play sound effect of locked door when pressed
                    }
                }
                 /*if (plyrLane == door.neededLane && door.neededLane == 3)
                 {
                     if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                     {
                         Debug.Log("lanes match");
                         if (Input.GetKeyDown(KeyCode.))
                         {
                             if (door.Key1Needed == true)
                             {
                                 if (GM.gotkey1 == true)
                                 {
                                     door.locked = false;
                                     touchingLockedDoor = false;
                                     touchingDoor = true;
                                     Debug.Log("door unlocked now");
                                     GM.gotkey1 = false;
                                 }
                             }
                             if (door.Key2Needed == true)
                             {
                                 if (GM.gotkey2 == true)
                                 {
                                     door.locked = false;
                                     touchingLockedDoor = false;
                                     touchingDoor = true;
                                     Debug.Log("door unlocked now");
                                     GM.gotkey2 = false;
                                 }
                             }
                             if (door.Key3Needed == true)
                             {
                                 if (GM.gotkey3 == true)
                                 {
                                     door.locked = false;
                                     touchingLockedDoor = false;
                                     touchingDoor = true;
                                     Debug.Log("door unlocked now");
                                     GM.gotkey3 = false;
                                 }
                             }
                             //will play sound effect of locked door when pressed
                         }
                 }*/
             }


                //setting speed for walking or sprinting
                if (Input.GetKey(KeyCode.LeftShift) && sprintCooldownManager <= 0 || Input.GetKey(KeyCode.RightShift) && sprintCooldownManager <= 0) //is player sprinting
                {
                    plyrWalkSpeed = sprintSpeedDefault;
                    if (isRunning == false)
                    {
                        isRunning = true;
                        sprintCooldown = sprintTime;
                    }

                }

                else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                {
                    plyrWalkSpeed = walkSpeedDefault;
                }

                //movement left and right
                float move = Input.GetAxis("Horizontal");
                myRB.velocity = new Vector2(move * plyrWalkSpeed, myRB.velocity.y);

                //facing left or right
                if (move >= 0.01 && facingRight == false)
                {
                    Flip();
                    facingRight = true;
                }
                else if (move <= -0.01 && facingRight == true)
                {
                    Flip();
                    facingRight = false;
                }

                //animate
                if (move == 0)
                {
                    SpriteAnim.SetInteger("animState", 0);
                }
                if (move > 0 || move < 0)
                {
                    SpriteAnim.SetInteger("animState", 1);
                }


                //picking up a message
                if (checkMessage == true)
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                    {
                        checkMessage = false;
                        Mes.SetNewMessage = true;
                    }
                }

                //breaking a wall
                if (nearBreakableWall == true)
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
                    {
                        pressE.SetActive(false);
                        mySound.clip = Crumble;
                        mySound.Play();
                        nearBreakableWall = false;
                        Destroy(BreakableWall);
                    }

                }

            }

            if (GM.overallPaused == true)
            {
                myRB.velocity = new Vector2(0, myRB.velocity.y);
                SpriteAnim.SetInteger("animState", 0);
           
             }

            inPlace = true;

        }
    

    void OnTriggerEnter2D (Collider2D myColl)
    {
        if (myColl.gameObject.CompareTag("Door")) //If player is in front of door
        {
            //Debug.Log("In Front of Door");
            door = myColl.GetComponent<DoorController>();
            if (door.locked == false)
            {
                //Debug.Log("unlocked");
                touchingDoor = true;
            }
            else if (door.locked == true)
            {
                touchingLockedDoor = true;
            }
           
        }

        if (myColl.gameObject.tag == "Breakable")
        {
            if (GM.gotHammer == true)
            {
                nearBreakableWall = true;
                pressE.SetActive(true);
                BreakableWall = myColl.gameObject;
            }
        }

        if (myColl.gameObject.CompareTag("MessagePickUp"))
        {
            Mes = myColl.GetComponent<MessageController>();
            checkMessage = true;
            pressE.SetActive(true);
        }
        
        if (myColl.CompareTag("Key1"))
        {
            GM.gotkey1 = true;
            
        }
        if (myColl.CompareTag("Key2"))
        {
            GM.gotkey2 = true;

        }
        if (myColl.CompareTag("Key3"))
        {
            GM.gotkey3 = true;

        }
        if (myColl.CompareTag("MapPickup"))
        {
            GM.gotMap = true;
            Destroy(myColl);
        }
        if (myColl.CompareTag("Switch"))
        {
            swtch = myColl.GetComponent<doorSwitch>();
            swtch.plyrInLine = true;
            Debug.Log("front of switch");
            pressE.SetActive(true);
        }
        if (myColl.CompareTag("flashlight"))
        {
            GM.gotLight = true;
        }
        //if (myColl.gameObject.tag != "Door")
        //{
        //    touchingDoor = false;
        //}
    }

    void OnTriggerExit2D (Collider2D myExit) //when player exits the boor's collider, sets the boolean to false
    {
        if (myExit.gameObject.tag == "Door")
        {
            //Debug.Log("exit = true");
            touchingDoor = false;
            touchingLockedDoor = false;
            
        }
        if (myExit.gameObject.tag == "MessagePickUp")
        {
            Debug.Log("exit = true");
            pressE.SetActive(false);
            checkMessage = false;
        }
        if (myExit.gameObject.tag == "Switch")
        {
            swtch = myExit.GetComponent<doorSwitch>();
            swtch.plyrInLine = false;
            pressE.SetActive(false);
            Debug.Log("gone from switch");
        }
        if (myExit.gameObject.tag == "Breakable")
        {
            if (GM.gotHammer == true)
            {
                nearBreakableWall = false;
                BreakableWall = null;
                pressE.SetActive(false);
            }
        }
    }

    //fip character left or right
    void Flip()
    {
        //facingRight = !facingRight; //commented out for time being
        Vector3 tempScale = SpriteChild.transform.localScale;
        tempScale.x *= -1f;
        SpriteChild.transform.localScale = tempScale;
    }

}
