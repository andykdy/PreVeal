using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class FollowerCow : MonoBehaviour
{
    enum CowState { Idle, Following}

    public float flock_range;
    public float avoid_range;
    public float speed;
    private CowState cur_state;
    private Rigidbody2D rgbd;
    private static float tolerance = 0.3f;
    private static float rot_speed = 0.25f;
    private float master_angle;
    private float master_speed;
    private float rot_rate;
    public GameObject veal;
    
    // Start is called before the first frame update
    void Start()
    {
        cur_state = CowState.Idle;
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        rot_rate = Random.Range(8f, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 player_pos = FindObjectOfType<BabyCowScript>().transform.position;
        if (cur_state == CowState.Idle)
        {
            rgbd.transform.Translate(Vector3.down * speed *Time.deltaTime);
            if (Vector2.Distance(player_pos, gameObject.transform.position) < flock_range)
            {
                cur_state = CowState.Following;
                FindObjectOfType<BabyCowScript>().AddScore(1);
            }
        }

        if (cur_state == CowState.Following)
        {
            if (Vector2.Distance(player_pos, gameObject.transform.position) < flock_range)
            {
                gameObject.transform.rotation = Quaternion.RotateTowards(rgbd.transform.rotation,
                    Quaternion.Euler(0, 0, master_angle), rot_rate);
                rgbd.transform.Translate(Vector3.up * master_speed * 0.9f);
            }
            else
            {
                float rot_val = 180 * Mathf.Atan((player_pos.y - rgbd.transform.position.y) /
                                           (player_pos.x - rgbd.transform.position.x)) / Mathf.PI;
                rot_val = player_pos.x - rgbd.transform.position.x < 0 ? rot_val + 90 : rot_val - 90;
                gameObject.transform.rotation =
                    Quaternion.RotateTowards(rgbd.transform.rotation, Quaternion.Euler(0, 0, rot_val), 5.0f);
                rgbd.transform.Translate(Vector3.up * master_speed * 0.9f);
                
            }
        }
    }
    
    public void ReceiveData(float rot_mag, float given_speed)
    {
        master_angle = rot_mag;
        master_speed = given_speed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "potato") {
            //DO PARTICLE EFFECT
            Instantiate(veal, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

