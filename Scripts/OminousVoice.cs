using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OminousVoice : MonoBehaviour {

    //====  Script References  ====
    public GameObject overallGameMasterObject; //oject loaded in from title scene
    MasterGameMaster TrueGM; //Master script that keeps record of things outside of the scene. Set to DontDestroyOnLoad and handles what level we're in, the volume levels, and other such things we may implement.

    //====  Audio Game Objects  ====
    public GameObject startSound;
    public GameObject AnotherForTheDay;

    //====  Time Management  ====
    float PlayTime;
    public bool LoadDone;

    // Use this for initialization
    void Start () {
        PlayTime = 0f;
        LoadDone = false;
        overallGameMasterObject = GameObject.Find("MasterScript");
        TrueGM = overallGameMasterObject.GetComponent<MasterGameMaster>();

        if (TrueGM.DeathCount == 0)
        {
            startSound.SetActive(true);
            AnotherForTheDay.SetActive(false);
        }

        else if (TrueGM.DeathCount >= 1)
        {
            startSound.SetActive(false);
            AnotherForTheDay.SetActive(true);
        }
	}

    void Update()
    {
        PlayTime += Time.deltaTime;

        if(PlayTime >= 5f)
        {
            LoadDone = true;
        
        }

        if (PlayTime >= 5f && Input.anyKeyDown || PlayTime >= 7f)
        {
            if (TrueGM.CurrentLevel == 1)
            {
                SceneManager.LoadScene("Level1");
            }

        }


    }
}
