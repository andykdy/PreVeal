﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class veal_count : MonoBehaviour
{
    private Text my_text;
    // Start is called before the first frame update
    void Start()
    {
        my_text = gameObject.GetComponent<Text>();
        my_text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        tractor t = FindObjectOfType<tractor>();
        if (t != null)
            my_text.text = "" + t.getScore();
    }
}
