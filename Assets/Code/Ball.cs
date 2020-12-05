using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

namespace Assets.Code
{
	public class Ball : MonoBehaviour
	{
		public GameObject ball;
		public Rigidbody2D rb;
		private AudioSource source;
		public AudioClip paddleHit;
		public AudioClip brickHit;
		public AudioClip wallHit;
		public Vector3 old_velocity;

		// Start is called before the first frame update
		void Start()
		{
			rb = ball.GetComponent<Rigidbody2D>();
			source = ball.GetComponent<AudioSource>();

		}

		// Update is called once per frame
		void Update()
		{
			if (gameObject.transform.position.y < -5)
			{
				Destroy(gameObject);
			}
		}


		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				source.clip = paddleHit;
				source.Play();
			} //TODO actually add clips for the following 2 sounds
			else if (collision.gameObject.tag == "Brick")
			{
				source.clip = brickHit;
				source.Play();
			}
			else if (collision.gameObject.tag == "Wall")
			{
				source.clip = wallHit;
				source.Play();
			}
		}


		//when the ball is initially shot off the paddle, 
		public void Fire()
		{
			rb.AddForce(transform.up * 200);
			rb.AddForce(transform.right * 2);
		}
		
	}
	
	public class BallsData : GameData
	{
		public List<BallData> Ball_list = new List<BallData>();

		public BallsData()
		{
			GameObject[] bs = GameObject.FindGameObjectsWithTag("Ball");
			foreach (GameObject ball in bs)
			{
				BallData data = new BallData();
				var ball_com = ball.GetComponent<Ball>();
				data.Position = ball_com.transform.position;
				data.Velocity = ball_com.rb.velocity;
				//Debug.Log(data.Position);
				Ball_list.Add(data);
				
			}

			//Debug.Log(Ball_list.Count);
		}

		public void LoadThis()
		{
			foreach (BallData data in Ball_list)
			{
				GameObject x = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/Ball", typeof(GameObject)), 
					data.Position, 
					Quaternion.identity);
				Ball ball_com = x.GetComponent<Ball>();
				ball_com.GetComponent<Rigidbody2D>().velocity = data.Velocity;
			}

		}
	}

	public class BallData : GameData
	{
		public Vector2 Position;

		public Vector2 Velocity;
	}
}

