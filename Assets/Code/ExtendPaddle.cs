using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using UnityEngine;

namespace Assets.Code
{
    public class ExtendPaddle : MonoBehaviour
    {
        public Transform tr;
        public SpriteRenderer sr;

        public GameObject paddle;

        public Rigidbody2D rb;

        public int hp;


        // Start is called before the first frame update
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (hp < 1)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                ProcessHitPlayer();
            }

            if (collision.gameObject.tag == "Ball")
            {
                ProcessHitBall();
            }
        }

        void ProcessHitPlayer()
        {
            // find paddle and scale it be bigger
            paddle = GameObject.FindGameObjectWithTag("Player");
            tr = paddle.GetComponent<Transform>();
            Vector3 scale = new Vector3(0.3f, 0.2f, 1f);
            tr.localScale = scale;
            hp--;
        }

        void ProcessHitBall()
        {
            // turn on gravity for the powerup block so it falls down
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            if (hp > 0)
            {
                GameManager.AddScore(1);
            }

        }

    }
}

