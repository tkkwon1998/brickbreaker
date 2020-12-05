using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

namespace Assets.Code
{
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
			if (hp > 3)
			{
				hp = 3;
			} //red brick, 3hp

			if (hp == 3)
			{
				sr.sprite = red;
			} //green brick, 2hp
			else if (hp == 2)
			{
				sr.sprite = green;
			} //blue brick, 1hp
			else if (hp == 1)
			{
				sr.sprite = blue;
			} //no more hp, destroy
			else
			{
				Destroy(gameObject);
			}
		}


		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.tag == "Ball")
			{
				ProcessHit();
			}
		}

		void ProcessHit()
		{
			//Right now this just lowers the hp, it could be modified to allow powerups to drop
			hp--;
			GameManager.AddScore(1);
		}

	}

	public class BricksData : GameData
	{
		public List<BrickData> Brickss = new List<BrickData>();
		public List<BrickData> EPadBricks = new List<BrickData>();
		public List<BrickData> EPoiBricks = new List<BrickData>();
		public List<BrickData> MBBricks = new List<BrickData>();

		public BricksData()
		{
			GameObject[] brick_list = GameObject.FindGameObjectsWithTag("Brick");
			foreach (GameObject brick in brick_list)
			{
				BrickData data = new BrickData();

				if (brick.name.Contains("ExtendPaddle"))
				{
					//Debug.Log(brick.name);
					var compon = brick.GetComponent<ExtendPaddle>();
					data.HP = compon.hp;
					data.Position = compon.transform.position;
					EPadBricks.Add(data);
				}
				else if (brick.name.Contains("ExtraPoints"))
				{
					//Debug.Log(brick.name);
					var compon = brick.GetComponent<ExtraPoints>();
					data.HP = compon.hp;
					data.Position = compon.transform.position;
					EPoiBricks.Add(data);
				}
				else if (brick.name.Contains("MoreBalls"))
				{
					//Debug.Log(brick.name);
					var compon = brick.GetComponent<MoreBalls>();
					data.HP = compon.hp;
					data.Position = compon.transform.position;
					MBBricks.Add(data);
				}
				else if (brick.name.Contains("Brick"))
				{
					//Debug.Log(brick.name);
					var compon = brick.GetComponent<Brick>();
					//Debug.Log(compon);
					data.HP = compon.hp;
					data.Position = compon.transform.position;
					Brickss.Add(data);
				}

			}
		}

		public void LoadThis()
		{
			foreach (BrickData data in Brickss)
			{
				GameObject x = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/Brick", typeof(GameObject)), 
					data.Position, 
					Quaternion.identity);
				var compon = x.GetComponent<Brick>();
				compon.hp = data.HP;
			}
			
			foreach (BrickData data in EPadBricks)
			{
				GameObject x = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/ExtendPaddle", typeof(GameObject)), 
					data.Position, 
					Quaternion.identity);
				var compon = x.GetComponent<ExtendPaddle>();
				compon.hp = data.HP;
			}
			
			foreach (BrickData data in EPoiBricks)
			{
				GameObject x = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/ExtraPoints", typeof(GameObject)), 
					data.Position, 
					Quaternion.identity);
				var compon = x.GetComponent<ExtraPoints>();
				compon.hp = data.HP;
			}
			
			foreach (BrickData data in MBBricks)
			{
				GameObject x = GameObject.Instantiate((GameObject) Resources.Load("Prefabs/MoreBalls", typeof(GameObject)), 
					data.Position, 
					Quaternion.identity);
				var compon = x.GetComponent<MoreBalls>();
				compon.hp = data.HP;
			}
		}
	}

	public class BrickData : GameData
	{
		public int HP;
		public Vector2 Position;
	}
}
