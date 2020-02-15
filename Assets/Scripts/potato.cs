using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potato : MonoBehaviour
{
    public float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y * speed * Time.deltaTime);

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > max.y) {
            Destroy(gameObject);

        }

        Vector2 movement = new Vector2(0, 1) * speed * Time.deltaTime;
        transform.Translate(movement);

    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "cow") {
            Destroy(gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
