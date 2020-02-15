using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    private Rigidbody2D rgbd;

    protected bool del_flag;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        rgbd.velocity = Vector2.down;
        del_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        del_flag = true;
    }

    public bool isDelete()
    {
        return del_flag;
    }
}
