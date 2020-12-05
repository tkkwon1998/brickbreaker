using System.Collections;
using System.Collections.Generic;
//using Assets.Code;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
	public class GameManager : MonoBehaviour
	{
		public GameObject Canvas;

		//the actual ball in play
		public Ball initialBall = null;

		//a prefab, assigned in the unity editor
		public Ball _b;

		//public MenuManager MM { get; private set; }

		//the paddle, assigned value in the unity editor
		public Paddle paddle;

		//has the ball left the paddle yet?
		public static bool fired;

		public bool loaded;

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

		public static bool started;

		//is game paused?
		public bool paused;

		//have all the objects been destroyed?
		private bool endHandled;

		//did you win or lose?
		private bool won;

		public GameObject Menu;

		private bool MenuUp;

		private SaveLoadThis start_scene;

		public SaveLoadThis SL;

		// Start is called before the first frame update
		void Start()
		{
			won = false;
			stopGame = false;
			fired = false;
			endHandled = false;
			paused = false;
			loaded = false;
			started = false;
			scoreNum = 0;
			//var menu = GameObject.Instantiate((GameObject)Resources.Load("Menus", typeof(GameObject)));
			//menu.transform.SetParent(transform, false);
			ShowMenu("MainMenu");

			//StartBall();
		}

		// Update is called once per frame
		void Update()
		{
			if (!MenuUp && !paused)
			{
				if (!stopGame)
				{
					HandleInput();
					BallFollow();
					DisplayScore();
					CheckOffScreen();
					CheckWin();
				}
				else
				{
					HandleEnd();
					if (Input.GetKeyDown(KeyCode.Q) && !loaded)
					{
						score.fontSize = 40;
						paddle.gameObject.SetActive(true);
						LoadStuff();
					}

					if (Input.GetKeyDown(KeyCode.Q))
					{
						
						loaded = false;
						stopGame = false;
						endHandled = false;
					}
				}
			}
		}


		//cleans up at the end of the game, displays final score
		void HandleEnd()
		{

			//we only wanna destroy objects once
			if (!endHandled)
			{
				//GameObject.Destroy(initialBall.gameObject);
				paddle.gameObject.SetActive(false);
				//GameObject.Destroy(paddle.gameObject);
				for (int i = 0; i < bricks.Length; i++)
				{
					GameObject.Destroy(bricks[i].gameObject);
				}

				endHandled = true;
			}

			//when the objects have been destroyed, update the score text to be endgame text
			score.fontSize = 90;
			if (won)
			{
				score.text = "You Won! Final Score: " + scoreNum;
			}
			else
			{
				score.text = "You Lost! Final Score: " + scoreNum;
			}

		}

		//checks if any bricks remain
		void CheckWin()
		{
			bricks = GameObject.FindGameObjectsWithTag("Brick");
			if (bricks.Length == 0)
			{
				stopGame = true;
				won = true;
			}

		}

		//checks if the ball is off screen
		void CheckOffScreen()
		{
			balls = GameObject.FindGameObjectsWithTag("Ball");
			if (balls.Length == 0)
			{
				stopGame = true;
			}
		}

		//shows the score on the screen
		void DisplayScore()
		{
			score.text = scoreNum + "";
		}

		//keeps the ball following the paddle, slightly above the paddle
		void BallFollow()
		{
			if (!started)
			{
				start_scene = new SaveLoadThis();
				start_scene.Save();
				StartBall();
				// start_scene.GetStartScene();
				started = true;
			} else if (!fired)
			{
				Vector3 paddlePos = paddle.gameObject.transform.position;
				Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .3f, 0);
				initialBall.transform.position = ballPos;
			}
		}

		//handles overall gameplay input
		void HandleInput()
		{
			if (Input.GetKey(KeyCode.Space))
			{
				FireBall();
			}

			if (Input.GetKey(KeyCode.Escape) && !paused)
			{
				paused = true;
				ShowMenu("PauseMenu");
			}

			if (Input.GetKeyDown(KeyCode.S) && !paused)
			{
				SL = new SaveLoadThis();
				SL.score = scoreNum;
				SL.Save();
			}
			
			if (Input.GetKeyDown(KeyCode.Q) && !loaded)
			{
				LoadStuff();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				loaded = false;
			}
		}

		//instatiates the ball just above the center of the paddle
		void StartBall()
		{
			Debug.Log("New Ball");
			Vector3 paddlePos = paddle.gameObject.transform.position;
			Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .3f, 0);
			initialBall = Instantiate(_b, ballPos, Quaternion.identity);
			initialBall.rb = initialBall.GetComponent<Rigidbody2D>();
		}

		//shoot the paddle off the ball
		void FireBall()
		{
			if (!fired)
			{
				fired = true;
				initialBall.Fire();
			}
		}


		//add to the score, variable amount for powerups
		//this is static, so you can call if from other scripts 
		//probably call from the OnCollisionEnter2D for the paddle/powerup script
		public static void AddScore(int amt)
		{
			scoreNum += amt;
		}

		public void ShowMenu(string menu)
		{
			if (menu == "MainMenu")
			{
				Menu = GameObject.Instantiate((GameObject) Resources.Load("Main Menu", typeof(GameObject)));
				Menu.transform.SetParent(Canvas.transform, false);
				MenuManager MM = Menu.GetComponent<MenuManager>();
				MM.Start_button.onClick.AddListener(() => HideMenu());
				MM.Instructions.onClick.AddListener(() => ShowMenu("MM"));
				MenuUp = true;

			}
			else if (menu == "PauseMenu")
			{
				Menu = GameObject.Instantiate((GameObject) Resources.Load("Pause Menu", typeof(GameObject)));
				Menu.transform.SetParent(Canvas.transform, false);
				MenuManager MM = Menu.GetComponent<MenuManager>();
				MM.Start_button.onClick.AddListener(() => ResumeGame());
				MM.Instructions.onClick.AddListener(() => ShowMenu("PM"));
				MM.Quit.onClick.AddListener(() => QuitGame());
				MenuUp = true;
				foreach (GameObject bal in balls)
				{
					Ball ball_com = bal.GetComponent<Ball>();
					ball_com.old_velocity = ball_com.rb.velocity;
					ball_com.rb.velocity = ball_com.rb.velocity * 0;
				}

			}
			else
			{

				HideMenu();
				Menu = GameObject.Instantiate((GameObject) Resources.Load("Instruction Menu", typeof(GameObject)));
				Menu.transform.SetParent(Canvas.transform, false);
				MenuManager MM = Menu.GetComponent<MenuManager>();
				MM.Back.onClick.AddListener(() => GoBack(menu));
				MenuUp = true;
			}
		}

		public void HideMenu()
		{
			Debug.Log("Hide Main");
			MenuUp = false;
			paused = false;
			GameObject.Destroy(Menu);
			Menu = null;
		}

		public void ResumeGame()
		{
			Debug.Log("Hide Pause Menu");
			paused = false;
			MenuUp = false;
			GameObject.Destroy(Menu);
			
			foreach (GameObject bal in balls)
			{
				Ball ball_com = bal.GetComponent<Ball>();
				ball_com.rb.velocity = ball_com.old_velocity;
				
				//Debug.Log(ball_com);
				// if (ball_com)
				// {
				// 	bal_com.rb.velocity = ball_com.old_velocity;
				// }
			}

			Menu = null;
		}

		public void GoBack(string prev_menu)
		{
			Debug.Log("Hide Pause Menu");
			MenuUp = false;
			GameObject.Destroy(Menu);
			Menu = null;
			if (prev_menu == "MM")
			{
				ShowMenu("MainMenu");
			}
			else
			{
				foreach (GameObject bal in balls)
				{
					var ball_com = bal.GetComponent<Ball>();
					ball_com.rb.velocity = ball_com.old_velocity;
				}
				ShowMenu("PauseMenu");
			}
		}

		public void QuitGame()
		{
			HideMenu();
			balls = GameObject.FindGameObjectsWithTag("Ball");
			bricks = GameObject.FindGameObjectsWithTag("Brick");
			foreach (GameObject b in balls)
			{
				b.SetActive(false);
				GameObject.Destroy(b);
			}
			foreach (GameObject b in bricks)
			{
				b.SetActive(false);
				GameObject.Destroy(b);
			}

			start_scene.Load();
			Start();
			scoreNum = 0;
		}

		public void LoadStuff()
		{
			balls = GameObject.FindGameObjectsWithTag("Ball");
			bricks = GameObject.FindGameObjectsWithTag("Brick");
			
			foreach (GameObject b in balls)
			{
				b.SetActive(false);
				GameObject.Destroy(b);
			}
			foreach (GameObject b in bricks)
			{
				b.SetActive(false);
				GameObject.Destroy(b);
			}
			SL.Load();
			scoreNum = SL.score;
			loaded = true;
		}

	}
}
