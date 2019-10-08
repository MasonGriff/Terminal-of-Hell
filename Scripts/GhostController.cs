using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    //====  References  ====
    GameMaster GM;
    public GameObject playerObject;
    public GameObject GMreference;
    PlayerController Player;
    public GameObject thisGhost;
    //public Transform yPositionMarker;
    public GameObject GhostSprite;
    public Animator GhostAnim;
    //Prefabs
    //public GameObject GhostAttackAnim;


    //====  Movement Basis  ====
    float patrolSpeed;
    Rigidbody2D myRB;
    public float normalPatrol = -1f;
    float inversePatrol;
    GameObject GhostLeft; //Universal object that sets the furthest all ghosts can go left.
    GameObject GhostRight; //Universal object that sets the furthest all ghosts can go right.

    //====  Other  ====
    public bool attacking = false;
    public float attackTime = 0f;
    
    


    // Use this for initialization
    void Start () {
        playerObject = GameObject.Find("Player"); //automatically sets the player Game Object variable to the object named Player
        GMreference = GameObject.Find("GM"); //automatically sets the GM Game Object variable to the object named GM
        GM = GMreference.GetComponent<GameMaster>();
        thisGhost = this.gameObject;
        myRB = GetComponent<Rigidbody2D>();
        GhostLeft = GameObject.Find("GhostFuthestLeft");
        GhostRight = GameObject.Find("GhostFuthestRight");
        patrolSpeed = normalPatrol;
        inversePatrol = -normalPatrol;
        Player = playerObject.GetComponent<PlayerController>();
        Vector3 startTartGo = transform.position;
        //startTartGo.y = yPositionMarker.position.y;
        //startTartGo.z = yPositionMarker.position.z;
        transform.position = startTartGo;
        GhostSprite = thisGhost.transform.Find("GhostSprite").gameObject;
        GhostAnim = GhostSprite.GetComponent<Animator>();
        GhostAnim.SetInteger("GhostState", 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (GM.overallPaused == false) { //as long as game isn't paused
            if (attacking == false)
            {
                GhostAnim.SetInteger("GhostState", 0);

                if (transform.position.x <= GhostLeft.transform.position.x)
                {
                    Debug.Log("Ghost reached End");
                    Flip();
                    patrolSpeed = inversePatrol;
                }
                else if (transform.position.x >= GhostRight.transform.position.x)
                {
                    Debug.Log("Ghost reached End");
                    Flip();
                    patrolSpeed = normalPatrol;
                }


                myRB.velocity = new Vector2(patrolSpeed * 1.5f, myRB.velocity.y);
            }
            if (attacking == true)
            {
                //Destroy(gameObject); //temporary until ghost animation can be fixed
                //GhostAnim.SetInteger("GhostState", 1);
                attackTime += Time.deltaTime;
                Vector3 playerPos = Player.transform.position;
                playerPos.z -= .5f;
                transform.position = playerPos;
            }

            if (attackTime >= 1f)
            {
                Destroy(gameObject);
            }

        }
        if (GM.overallPaused == true)
        {
            myRB.velocity = new Vector2(0, myRB.velocity.y);
        }
    }

    void OnCollisionEnter2D (Collision2D myColl)
    {
        if (myColl.gameObject.CompareTag("Protagonist"))
        {
            if (attacking == false)
            {
                Vector3 playerPos = Player.transform.position;
                playerPos.z -= .5f;
                transform.position = playerPos;
                Destroy(this.gameObject.GetComponent<Rigidbody2D>());
                BoxCollider2D myCollider = this.gameObject.GetComponent<BoxCollider2D>();
                myCollider.enabled = false;
                
                Player.Health -= 1;
                GhostAnim.SetInteger("GhostState", 1);
                attacking = true;
                
                //Destroy(gameObject);
            }
        }
    }

    void Flip()
    {
        //facingRight = !facingRight; //commented out for time being
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1f;
        transform.localScale = tempScale;
    }

}
