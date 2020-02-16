using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public void LevelOne()
    {
        Application.LoadLevel("Level1");
    }

    public void LevelTwo()
    {
        Application.LoadLevel("Level2");
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
