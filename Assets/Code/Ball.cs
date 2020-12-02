using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public GameObject ball;
	private Rigidbody2D rb;
	private AudioSource source;
	public AudioClip paddleHit; 
	public AudioClip brickHit;
	public AudioClip wallHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
        source = ball.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -5){
    		Destroy(gameObject);
    	}
    }


    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Player"){
    		source.clip = paddleHit;
    		source.Play();
    	}//TODO actually add clips for the following 2 sounds
    	else if (collision.gameObject.tag == "Brick"){
    		source.clip = brickHit;
    		source.Play();
    	}
    	else if (collision.gameObject.tag == "Wall"){
    		source.clip = wallHit;
    		source.Play();
    	}
    }


    //when the ball is initially shot off the paddle, 
    public void Fire(){
    	rb.AddForce(transform.up*200);
    	rb.AddForce(transform.right*2);
    }
}
