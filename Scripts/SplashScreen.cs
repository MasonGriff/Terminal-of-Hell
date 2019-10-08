using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public GameObject splash1;
    bool splash1up = false;
    float splashCount = 0;

	// Use this for initialization
	void Start () {
        splash1.transform.DOScale(1f, 2);
	}
	
	// Update is called once per frame
	void Update () {

        if (splash1up == false && splash1.transform.localScale.magnitude < 1)
        {
            splash1.transform.DOScale(1f, 2);
        }
        if (splash1up == true)
        {
            splashCount += Time.deltaTime;
        }
        if (Input.anyKey)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        if (splash1up == false)
        {
            splash1.transform.DOScale(1f, 2);
            if (splash1.transform.localScale.magnitude == 1)
            {
                splash1up = true;
            }
        }
        if(splash1up == true && splashCount >= 5)
        {
            splash1.transform.DOScale(0f,1);
        }

        if(splash1up == true && splashCount >= 6)
        {
            SceneManager.LoadScene("TitleScreen");
        }
	}
}
