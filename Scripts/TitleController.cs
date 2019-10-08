using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleController : MonoBehaviour {

    //====  Game Objects  ====
    public GameObject PressStart;
    public GameObject Logo;
    public GameObject Buttons;
    public GameObject CharacterSprite;

    //====  Variables  ====
    bool startPressed = false;
    public float DoScaleTime = 1.5f;


	// Use this for initialization
	void Start () {
        PressStart.transform.DOScaleY(1, DoScaleTime);
        Logo = GameObject.Find("Logo");
    }
	
	// Update is called once per frame
	void Update () {
		if (startPressed == false && PressStart.transform.localScale.y == 1)
        {
            if (Input.anyKeyDown)
            {
                startPressed = true;
                PressStart.transform.DOScaleY(0, DoScaleTime * 0.5f);
                Logo.transform.DOMoveY(3.5f, DoScaleTime);
                Logo.transform.DOScale(0.6f, DoScaleTime);
                CharacterSprite.transform.DOMoveX(6.62f, DoScaleTime * 2);
                Buttons.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
