using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreBalls : MonoBehaviour
{
    // ball prefab
    public Ball extraBall;
    public Ball _b;
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
        // call startball, function that instantiates and shoots new ball
        StartBall();
        hp--;
    }

    void ProcessHitBall() {
        // turn on gravity for the powerup block so it falls down
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
    	GameManager.AddScore(1);
    }

    void StartBall(){
    	Vector3 paddlePos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
    	Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .3f,0);
    	extraBall = Instantiate(_b, ballPos, Quaternion.identity);
        extraBall.GetComponent<Rigidbody2D>().AddForce(transform.up*200);
    }
}