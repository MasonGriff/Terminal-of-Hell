using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    private GameObject breakableWall;
    public GameObject playerObj;
    PlayerController plyr;

    void Start()
    {
        playerObj = this.gameObject;
        plyr = playerObj.GetComponent<PlayerController>();
    }


    bool CheckCloseToTag(string tag, float minimumDistance)
    {
        GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < goWithTag.Length; ++i)
        {
            if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
            { 
                Debug.Log("Wall is Breakable");
                plyr.pressE.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) //If "E" is pressed when near "Breakable" wall, the wall is destroyed.
                {
                    breakableWall = GameObject.FindWithTag("Breakable");
					plyr.pressE.SetActive(false);
                    Destroy(breakableWall);
                }
            }
                return true;
            
        }
        plyr.pressE.SetActive(false);
        return false;
    }

    void Update()
    {
        CheckCloseToTag("Breakable", 3); //Finds all objects with a specific tag within a minimum distance from the player.
    }
}
