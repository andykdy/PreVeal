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
    public GameObject player_prefab;
    private CowState cur_state;
    private Rigidbody2D rgbd;
    private static float tolerance = 0.3f;
    private static float rot_speed = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        cur_state = CowState.Idle;
        rgbd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cur_state == CowState.Idle)
        {
            rgbd.transform.Translate(Vector3.down * speed *Time.deltaTime);
            if (Vector2.Distance(player_prefab.transform.position, gameObject.transform.position) < 2)
            {
                cur_state = CowState.Following;
            }
        }

        if (cur_state == CowState.Following)
        {
            FlockRoutine(GameObject.FindGameObjectsWithTag("cow"));
        }
    }
    
    private void FlockRoutine(GameObject[] cows)
    {
        List<Vector3> nearby_pos = new List<Vector3>();
        List<float> nearby_rot = new List<float>();
        for (int i = 0; i < cows.Length; i++)
        {
            if (Vector2.Distance(cows[i].transform.position, gameObject.transform.position) < flock_range)
            {
                nearby_pos.Add(cows[i].transform.position);
                nearby_rot.Add(cows[i].transform.eulerAngles.z);
            }
        }
        Debug.Log(nearby_rot.Average()-180);
        gameObject.transform.rotation = Quaternion.RotateTowards(rgbd.transform.rotation, Quaternion.Euler(0, 0, nearby_rot.Average()), 0.25f);
    }
}

