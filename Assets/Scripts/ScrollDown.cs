using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDown : MonoBehaviour
{
	public float speed;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, -1 * position.y * speed * Time.deltaTime);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y) {
            Destroy(gameObject);
        }

        Vector2 pos = transform.position;

        transform.position = new Vector3(pos.x, pos.y - speed * Time.deltaTime, 0);
    }
}
