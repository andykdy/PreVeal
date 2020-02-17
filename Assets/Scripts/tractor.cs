using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractor : MonoBehaviour
{
    public float speed;
    public GameObject potato;
    public float timeLeft;
    public GameObject cowAudio;
    public GameObject gunAudio;

    private int potatoes;
    private int score;
    private float kill_down = -1f;
    private float drag_time = 0f;
    private GameObject cowsfx;
    private GameObject gunsfx;
    public ParticleSystem potatoBomb;

    // Start is called before the first frame update
    void Start()
    {
        potatoes = 3;
        score = 0;
        cowsfx = Instantiate(cowAudio);
        gunsfx = Instantiate(gunAudio);
    }



    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (kill_down > 0)
            kill_down -= Time.deltaTime;
        if (drag_time > 0)
        {
            drag_time -= Time.deltaTime;
            speed = 1f;
        }
        else
            speed = 5f;

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow)) && GameOver.isInputEnabled)
        {

            //Movement with what button direction * speed I want * Time.deltaTime to make it run per second
            Vector2 movement = new Vector2(1, 0) * speed * Time.deltaTime;

            //the movement
            transform.Translate(movement);

        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.LeftArrow)) && GameOver.isInputEnabled)
        {

            //Movement with what button direction * speed I want * Time.deltaTime to make it run per second
            Vector2 movement = new Vector2(-1, 0) * speed * Time.deltaTime;

            //the movement
            transform.Translate(movement);

        }
        

        //bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var top = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, left, right),
            Mathf.Clamp(transform.position.y, top, bottom),
            transform.position.z);


        if (Input.GetKeyDown(KeyCode.UpArrow) && potatoes > 0 && timeLeft < 0  && GameOver.isInputEnabled)
        {
            Instantiate(potato, transform.position, Quaternion.identity);
            gunsfx.GetComponent<AudioSource>().Play();
            potatoes -= 1;
            timeLeft = 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<FollowerCow>() != null)
        {
            FollowerCow cow = other.gameObject.GetComponent<FollowerCow>();
            cowsfx.GetComponent<AudioSource>().Play();
            ParticleSystem explosion = Instantiate(cow.bloodBomb);
            explosion.transform.position = transform.position;
            if (cow.GetState() == FollowerCow.CowState.Following)
            {
                FindObjectOfType<BabyCowScript>().AddScore(-1);
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.GetComponent<BabyCowScript>() != null && kill_down < 0)
        {
            kill_down = 0.5f;
            cowsfx.GetComponent<AudioSource>().Play();
            ParticleSystem explosion = Instantiate(other.gameObject.GetComponent<BabyCowScript>().bloodBomb);
            explosion.transform.position = other.gameObject.GetComponent<BabyCowScript>().transform.position;
            other.gameObject.GetComponent<BabyCowScript>().AddHealth(-1);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "potato_sack")
        {
            potatoes += 5;
            Destroy(col.gameObject);
        }
        else if (col.tag == "fence")
        {
            if (potatoes > 2) {
                potatoes -= 3;
            }
            else
            {
                potatoes = 0;
            }
            ParticleSystem explosion = Instantiate(potatoBomb);
            explosion.transform.position = transform.position;
            Destroy(col.gameObject);
            
        }
        else if (col.tag == "mud")
        {
            drag_time = 5f;
        }
        else if (col.tag == "veal") {
            score++;
            Destroy(col.gameObject);
        }
    }

    public int getScore() 
    {
        return score;
    }

    public int getPotatoCount() 
    {
        return potatoes;
    }
}
