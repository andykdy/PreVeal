using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private Camera myCam;
    private Rigidbody2D rgbd;
    private LinkedList<GameObject> back_img;
    

    public GameObject background_prefab;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        float height = 2f * myCam.orthographicSize;
        GameObject init_upper = Instantiate(background_prefab, new Vector3(0, height/2), Quaternion.identity);
        GameObject init_lower = Instantiate(background_prefab, new Vector3(0, -height/2), Quaternion.identity);
        
        back_img = new LinkedList<GameObject>();
        back_img.AddLast(init_lower);
        back_img.AddLast(init_upper);
    }

    // Update is called once per frame
    void Update()
    {
        float height = 2f * myCam.orthographicSize;
        if (back_img.First().GetComponent<BackgroundBehaviour>().isDelete())
        {
            GameObject returned = back_img.First();
            back_img.RemoveFirst();
            Destroy(returned);
            GameObject replacement = Instantiate(background_prefab, new Vector3(0, height), Quaternion.identity);
            back_img.AddLast(replacement);
        }
    }
}
