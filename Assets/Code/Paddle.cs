using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

namespace Assets.Code
{
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
			if (!GameManager.stopGame)
			{
				HandleInput();
			}
		}

		void HandleInput()
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				//right behavior
				transform.Translate(new Vector3(3 * Time.deltaTime, 0, 0));
			}

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				//left behavior
				transform.Translate(new Vector3(-3 * Time.deltaTime, 0, 0));
			}
		}


	}
	public class PaddleData : GameData
	{
		public Vector2 Position;
		public Vector2 scale;

		public PaddleData()
		{
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			scale = p.transform.localScale;
			Position = p.transform.position;
		}
		
		public void LoadThis()
		{
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			Vector3 new_scale = scale;
			p.transform.localScale = new_scale + new Vector3(0f, 0f, 1f);
			p.transform.position = Position;
		}
	}
}
