using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text my_score;
    // Start is called before the first frame update
    void Start()
    {
        my_score = gameObject.GetComponent<Text>();
        my_score.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        BabyCowScript player = FindObjectOfType<BabyCowScript>();
        my_score.text = "Score: " + player.GetScore();
    }
}
