using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPoints : MonoBehaviour
{
    public Transform tr;
    public SpriteRenderer sr;

    public GameObject paddle;

    public Rigidbody2D rigidbody;

    public int hp;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 1){
        	Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Player"){
    		ProcessHitPlayer();
    	}
        if(collision.gameObject.tag == "Ball"){
    		ProcessHitBall();
    	}
    }

    void ProcessHitPlayer(){
        // add 5 points if player gets powerup
        hp--;
        GameManager.AddScore(5);
    }

    void ProcessHitBall() {
        // turn on gravity for the powerup block so it falls down
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        if (hp > 0) { 
            GameManager.AddScore(1);
        }

    }
}

