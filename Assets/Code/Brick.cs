using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	//different displays for different hp amounts
	public Sprite blue;
	public Sprite red;
	public Sprite green;

	public SpriteRenderer sr;

	//set the HP in the unity editor
	public int hp;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    	//we dont have bricks over 3 hp
        if(hp > 3){
        	hp = 3;
        }//red brick, 3hp
        if(hp == 3){
        	sr.sprite = red;
        }//green brick, 2hp
        else if (hp ==2){
        	sr.sprite = green;
        }//blue brick, 1hp
        else if (hp == 1){
        	sr.sprite = blue;
        }//no more hp, destroy
        else{
        	Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision){
    	if(collision.gameObject.tag == "Ball"){
    		ProcessHit();
    	}
    }

    void ProcessHit(){
    	//Right now this just lowers the hp, it could be modified to allow powerups to drop
    	hp--;
    	GameManager.AddScore(1);
    }
}
