using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Loading : MonoBehaviour {

    //====  references  ====
    public GameObject voiceojb;
    OminousVoice VoiceControl;

    //====  game objects  ====
    public GameObject loadText;
    public GameObject LoadCircle;
    //public GameObject LoadingFinishedText;
    //Vector3 rotatePoint;

	// Use this for initialization
	void Start () {
        voiceojb = GameObject.Find("Ominous voice");
        VoiceControl = voiceojb.GetComponent<OminousVoice>();
        //rotatePoint = LoadCircle.transform.rotation.eulerAngles;
        //rotatePoint.y = 1440f;
	}
	
	// Update is called once per frame
	void Update () {
		if (VoiceControl.LoadDone == false)
        {
            //LoadCircle.transform.DORotate(rotatePoint, 5);

        }
        else if (VoiceControl.LoadDone == true)
        {
            loadText.transform.DOScale(0f, 0.5f);
            LoadCircle.transform.DOScale(0f, 0.5f);
           // LoadingFinishedText.SetActive(true);
        }
	}
}
