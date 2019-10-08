using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

    public GameObject overallGameMasterObject; //oject loaded in from title scene
    MasterGameMaster TrueGM; //Master script that keeps record of things outside of the scene. Set to DontDestroyOnLoad and handles what level we're in, the volume levels, and other such things we may implement.
    public bool tester;

    // Use this for initialization
    void Start () {
        overallGameMasterObject = GameObject.Find("MasterScript");
        TrueGM = overallGameMasterObject.GetComponent<MasterGameMaster>();
        

        if (!tester)
        {
            TrueGM.CurrentLevel = 1;
            SceneManager.LoadScene("Pre-Level1");
        }
        if (tester)
        {
            TrueGM.CurrentLevel = -1;
            SceneManager.LoadScene("_PlayerMovementTesting0");
        }

    }
}
