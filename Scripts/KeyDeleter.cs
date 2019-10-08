using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDeleter : MonoBehaviour {

    public GameObject GMreference; //references the game object holding the Game Master script
    GameMaster GM; //the script which controlls universal functions of the game
    public int Keycodes = 1;

    // Use this for initialization
    void Start () {
        GMreference = GameObject.Find("GM");
        GM = GMreference.GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update () {
		if (GM.gotkey1 == true && Keycodes == 1)
        {
            Destroy(gameObject);
        }

        if (GM.gotkey2 == true && Keycodes == 2)
        {
            Destroy(gameObject);
        }

        if (GM.gotkey3 == true && Keycodes == 3)
        {
            Destroy(gameObject);
        }

        if (GM.gotMap == true && Keycodes == 0)
        {
            Destroy(gameObject);
        }
        if (GM.gotLight == true && Keycodes == -1)
        {
            Destroy(gameObject);
        }
    }
}
