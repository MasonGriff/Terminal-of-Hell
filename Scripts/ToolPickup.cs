using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickup : MonoBehaviour
{
    public GameObject GMreference; //references the game object holding the Game Master script
    GameMaster GM; //the script which controlls universal functions of the game
    public GameObject HammerUI;
    PlayerController controlOfPlay;
    public GameObject playerChar;

    //public GameObject Key1;


    void Start()
    {
        GMreference = GameObject.Find("GM");
        GM = GMreference.GetComponent<GameMaster>();
        playerChar = this.gameObject;
        controlOfPlay = playerChar.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hammer") //Enables the HammerController script if player enters trigger. Also DESTROYS hammer prefab.
        {
            //HammerController hammerController = gameObject.GetComponent<HammerController>();
            //hammerController.enabled = true;
            GM.gotHammer = true;
            HammerUI.gameObject.SetActive(true); //Shows HammerUI on HUD.

            Destroy(other.gameObject);
        }

        /*if (other.gameObject.tag == "Key") //Enables the KeyController script if player enters the trigger of any key. Also DISABLES the respective key gameObject.
        {
            KeyController keyController = gameObject.GetComponent<KeyController>();
            keyController.enabled = true;

            Key1.gameObject.SetActive(true); //Shows Key1 on HUD.

            other.gameObject.SetActive(false); //Disables key to signify player has collected it.
        }
    }*/
    }
}  
