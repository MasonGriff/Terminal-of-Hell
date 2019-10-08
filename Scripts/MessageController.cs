using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {

    public GameObject playerObject; //References the object that holds the player controller script
    PlayerController player; //references the script controlling the player
    public GameObject GMreference;
    GameMaster GM;
    public Sprite Message; //This is the image that gets displayed for the message. For the time being, each message is a seperate image. Set this to the sprite of the image needed.
    
    public bool SetNewMessage;

    // Use this for initialization
    void Start () {
        GMreference = GameObject.Find("GM");
        GM = GMreference.GetComponent<GameMaster>();
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerController>();
        SetNewMessage = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(SetNewMessage == true)
        {
            NewMessage();
        }
	}

    public void NewMessage()
    {
        GM.overallPaused = true;
        GM.ViewingMessage = true;
        GM.MessageWindow.SetActive(true);
        GM.MessageWindow.gameObject.GetComponent<Image>().sprite = Message;
        SetNewMessage = false;
        Debug.Log("Message is Up");
    }

}
