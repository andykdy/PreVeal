using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyCowScript : MonoBehaviour
{
	public float speed;
	public float slowModifier = 5f;
	private int my_score;
	// Start is called before the first frame update
	void Start()
	{
		my_score = 0;
	}

	// Update is called once per frame
	void Update()
	{
		// for testing
		if (Input.GetKey(KeyCode.Space)) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * slowModifier);
		}
		float rot_dir = 0;
		float cur_speed = 0;
		if (Input.GetKey(KeyCode.S)) 
			rot_dir = 180;
		if (Input.GetKey(KeyCode.A)) 
			rot_dir = 90;
		if (Input.GetKey(KeyCode.D)) 
			rot_dir = 270;
		gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(0,0,rot_dir), 1f);
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			cur_speed = speed * Time.deltaTime;
			transform.Translate(Vector2.up * cur_speed);
			
		}

		foreach (FollowerCow fc in FindObjectsOfType<FollowerCow>())
		{
			fc.ReceiveData(gameObject.transform.rotation.eulerAngles.z, cur_speed);
		}
		
		// main camera
		var dist = (transform.position - Camera.main.transform.position).z;

		var left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		var top = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
		var bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, left, right),
			Mathf.Clamp(transform.position.y, top, bottom),
			transform.position.z);
	}

	void onTriggerEnter2D(Collider2D col) {
		// cows
		if (col.tag == "cow") {
			// TODO solve the Cow Packing Problem
			// we should increment the score when we add a cow
			// with a maximum threshold
		}

		if (col.tag == "potato") {
			my_score--;
			if (my_score == 0) {
				Destroy(gameObject);
			}
		}

		if (col.tag == "mud") {
			// gameObject.AddComponent<Rigidbody2D>().addForce(transform.down * slowModifier);
		}

		// TODO show game over when cow dies
	}

	public void AddScore(int val)
	{
		my_score += val;
	}

	public int GetScore()
	{
		return my_score;
	}
}
