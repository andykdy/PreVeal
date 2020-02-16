using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cow_health : MonoBehaviour
{
    private Text my_text;
    // Start is called before the first frame update
    void Start()
    {
        my_text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        BabyCowScript player = FindObjectOfType<BabyCowScript>();
        if (player != null)
            my_text.text = "" + player.GetHealth();
        else
        {
            my_text.text = "0";
            var gameOver = FindObjectOfType<GameOver>();
            if (!gameOver.isShown()) {
                gameOver.ShowButtons();
            }
        }
    }
}