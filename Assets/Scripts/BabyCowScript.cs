using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Transactions;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BabyCowScript : MonoBehaviour
{
	public float speed;
	public float slowModifier = 5f;
	public float stun_timer;
	private int my_score;
	private int my_health;
	public ParticleSystem bloodBomb;
	private float stunned_cd;

	// Start is called before the first frame update
	void Start()
	{
		my_score = 0;
		my_health = 3;
		stunned_cd = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		var gameOver = FindObjectOfType<GameOver>();
        if (my_health == 0 && !gameOver.isShown())
        {
            SpawnManager spawner = FindObjectOfType<SpawnManager>();
            if (spawner != null) {
            	spawner.EndGame();
            }

            GameObject[] cows = GameObject.FindGameObjectsWithTag("follower_cow");
            foreach (GameObject cow in cows)
            {
                Destroy(cow);
            }

            Destroy(gameObject);
        }
        float rot_dir = 0;
		float cur_speed = 0;
		if (Input.GetKey(KeyCode.S) && GameOver.isInputEnabled) 
			rot_dir = 180;
		if (Input.GetKey(KeyCode.A) && GameOver.isInputEnabled)
			rot_dir = 90;
		if (Input.GetKey(KeyCode.D) && GameOver.isInputEnabled)
			rot_dir = 270;
		if (stunned_cd < 0.0f)
		{
			gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0,0,rot_dir), 10f);
			if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
			    Input.GetKey(KeyCode.D)) && GameOver.isInputEnabled)
			{
				cur_speed = speed * Time.deltaTime;
				transform.Translate(Vector2.up * cur_speed);
			}

			Vector2 rot_vec = gameObject.transform.up;
			foreach (FollowerCow fc in FindObjectsOfType<FollowerCow>())
			{
				float angle = Vector2.Angle(rot_vec, fc.transform.position - gameObject.transform.position);
				if (angle< 90)
					fc.ReceiveData(gameObject.transform.rotation.eulerAngles.z, cur_speed * 0.5f);
				else
					fc.ReceiveData(gameObject.transform.rotation.eulerAngles.z, cur_speed);
			}
		}
		else
		{
			gameObject.transform.Rotate(new Vector3(0,0,10));
			foreach (FollowerCow fc in FindObjectsOfType<FollowerCow>())
			{
				fc.ReceiveData(gameObject.transform.rotation.eulerAngles.z, 0);
			}
		}
		
		// main camera
		var dist = (transform.position - Camera.main.transform.position).z;

		var left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		var top = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
		var bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, left, right),
			Mathf.Clamp(transform.position.y, top + 1, bottom - 1),
			transform.position.z);
		stunned_cd -= Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "potato") {
			my_health--;
            ParticleSystem explosion = Instantiate(bloodBomb);
			explosion.GetComponent<AudioSource>().Play();
            explosion.transform.position = transform.position;
        }

		if (col.tag == "mud") {
			speed = 0.5f;
			//gameObject.GetComponent<Rigidbody2D>().addForce(transform.down * slowModifier);
		}
		
		if (col.tag == "fence")
		{
			stunned_cd = stun_timer;
			
			//gameObject.GetComponent<Rigidbody2D>().addForce(transform.down * slowModifier);
		}
		if (col.tag == "finish")
		{
			var gameOver = FindObjectOfType<GameOver>();
			gameOver.CowWins();
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "mud") {
			speed = 5f;
		}
	}

	public void AddScore(int val)
	{
		my_score += val;
	}

	public int GetScore()
	{
		return my_score;
	}

	public void AddHealth(int val)
	{
		my_health += val;
	}

	public int GetHealth()
	{
		return my_health;
	}
}
