using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

	public GameObject paddle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	if(!GameManager.stopGame){
        	HandleInput();
    	}
    }

    void HandleInput(){
    	if(Input.GetKey(KeyCode.RightArrow)){
        	//right behavior
        	transform.Translate(new Vector3(3*Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
        	//left behavior
        	transform.Translate(new Vector3(-3*Time.deltaTime,0,0));
        }
    }

    
}
