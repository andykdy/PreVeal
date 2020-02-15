using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyCowScript : MonoBehaviour
{
    public float speed = 10f;
    public float slowModifier = 5f;
    public int score = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // for testing
        if (Input.GetKey(KeyCode.Space)) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * slowModifier);
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate (Vector2.up * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Translate (Vector2.left * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate (Vector2.down * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Translate (Vector2.right * speed * Time.deltaTime);
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
            score--;
            if (score == 0) {
                Destroy(gameObject);
            }
        }

        if (col.tag == "mud") {
            // gameObject.AddComponent<Rigidbody2D>().addForce(transform.down * slowModifier);
        }

        // TODO show game over when cow dies
    }
}
