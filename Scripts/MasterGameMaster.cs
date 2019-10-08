using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterGameMaster : MonoBehaviour {

    static bool masterCreated = false;
    public int CurrentLevel = 0;
    public int DeathCount = 0;
    public bool gotLightMaster;

	// Use this for initialization
	void Awake()
    {
        if (masterCreated)
        {
            Destroy(this.gameObject);
        }
        if (!masterCreated)
        {
            DontDestroyOnLoad(this.gameObject);
            masterCreated = true;
        }
        
    }
}
