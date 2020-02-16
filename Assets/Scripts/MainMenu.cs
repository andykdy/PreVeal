using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        Application.LoadLevel("MainScene");
    }

    public void LevelSelect()
    {
        Application.LoadLevel("LevelSelect");
    }

    public void Help()
    {
        Application.LoadLevel("HelpMenu");
    }
}
