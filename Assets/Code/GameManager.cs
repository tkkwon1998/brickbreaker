using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//the actual ball in play
	public Ball initialBall;
	//a prefab, assigned in the unity editor
	public Ball _b;


	//the paddle, assigned value in the unity editor
	public Paddle paddle;

	//has the ball left the paddle yet?
	public static bool fired;

	//the text display for the score, doubles as "you won/lost" text
	public Text score;
	//the value of the score
	public static int scoreNum;
	//a list of all bricks on the screen
	public GameObject[] bricks;

	// list of all balls on screen
	public GameObject[] balls;


	//has the game stopped for some reason?
	//right now this is just for endgame, but could be modified for pause too
	//its static, so it should be accessible from the other scripts (stop paddle from moving)
	public static bool stopGame;

	//have all the objects been destroyed?
	private bool endHandled;

	//did you win or lose?
	private bool won;

    // Start is called before the first frame update
    void Start()
    {
    	won = false;
    	stopGame = false;
    	fired = false;
    	endHandled = false;
    	scoreNum = 0;
        StartBall();
    }

    // Update is called once per frame
    void Update()
    {
    	if(!stopGame){
	    	HandleInput();
	    	BallFollow();
	    	DisplayScore();
	    	CheckOffScreen();
	    	CheckWin();
		}
		else{
			HandleEnd();
		}
    }


    //cleans up at the end of the game, displays final score
    void HandleEnd(){

    	//we only wanna destroy objects once
    	if(!endHandled){
	    	//GameObject.Destroy(initialBall.gameObject);
	    	GameObject.Destroy(paddle.gameObject);
	    	for(int i = 0; i < bricks.Length; i++){
	    		GameObject.Destroy(bricks[i].gameObject);
	    	}
	    	endHandled = true;
	    }

	    //when the objects have been destroyed, update the score text to be endgame text
	    score.fontSize = 90;
	    if(won)
	    {
	    	score.text = "You Won! Final Score: " + scoreNum;
	    }
	    else
	    {
	    	score.text = "You Lost! Final Score: " + scoreNum;
	    }

    }

    //checks if any bricks remain
    void CheckWin(){
    	bricks = GameObject.FindGameObjectsWithTag("Brick");
    	if(bricks.Length == 0){
    		stopGame = true;
    		won = true;
    	}

    }

    //checks if the ball is off screen
    void CheckOffScreen(){
		balls = GameObject.FindGameObjectsWithTag("Ball");
    	if(balls.Length == 0){
    		stopGame=true;
    	}
    }

    //shows the score on the screen
    void DisplayScore(){
    	score.text = scoreNum + "";
    }

    //keeps the ball following the paddle, slightly above the paddle
    void BallFollow(){
    	if(!fired){
    		Vector3 paddlePos = paddle.gameObject.transform.position;
    		Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .3f,0);
    		initialBall.transform.position = ballPos;
    	}
    }

    //handles overall gameplay input
    void HandleInput(){
    	if(Input.GetKey(KeyCode.Space)){
        	FireBall();
        }

        //TODO handle pause
    }

    //instatiates the ball just above the center of the paddle
    void StartBall(){
    	Vector3 paddlePos = paddle.gameObject.transform.position;
    	Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .3f,0);
    	initialBall = Instantiate(_b, ballPos, Quaternion.identity);
    }

    //shoot the paddle off the ball
    void FireBall(){
    	if(!fired){
    		fired = true;
    		initialBall.Fire();
    	}
    }


    //add to the score, variable amount for powerups
    //this is static, so you can call if from other scripts 
    //probably call from the OnCollisionEnter2D for the paddle/powerup script
    public static void AddScore(int amt){
    	scoreNum += amt;
    }

}
