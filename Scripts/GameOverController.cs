using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    public GameObject overallGameMasterObject; //oject loaded in from title scene
    MasterGameMaster TrueGM;
    public GameObject FirstDeath;
    public GameObject multipleDeaths;
    public GameObject presstocontinue;
    int blinking;
    bool blinkoff = false;

    // Use this for initialization
    void Start () {
        overallGameMasterObject = GameObject.Find("MasterScript");
        TrueGM = overallGameMasterObject.GetComponent<MasterGameMaster>();
        if (TrueGM.DeathCount <= 1)
        {
            FirstDeath.SetActive(true);
        }
        else if (TrueGM.DeathCount > 1)
        {
            multipleDeaths.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (TrueGM.CurrentLevel == -1)
            {
                SceneManager.LoadSceneAsync("_PlayerMovementTesting0");

                //SceneManager.UnloadSceneAsync("_PlayerMovementTesting0");
            }
            if (TrueGM.CurrentLevel == 1)
            {
                SceneManager.LoadScene ("Pre-Level1");
                //SceneManager.UnloadSceneAsync("Level1");
            }
        }
    }

    /*void FixedUpdate()
    {
        if (blinking == 60 && blinkoff == false)
        {
            blinkoff = true;
            blinking = 0;
            presstocontinue.SetActive(false);
        }
        else if (blinking == 60 && blinkoff == true)
        {
            blinkoff = false;
            blinking = 0;
            presstocontinue.SetActive(true);
        }
        blinking++;*/
    //}
}
