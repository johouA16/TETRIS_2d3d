using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameOverFlag
{
    public static bool End_flag = false;
}

public class Game_Start_End : MonoBehaviour
{
    public Text gametext;
    float start_time = 0;

    void Start()
    {
        Debug.Log("Display GAME START");
        gametext.text = "GAME START";
        start_time = Time.time;
        GameOverFlag.End_flag = false;
        Group2d.fallTime = 1.0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || (Time.time - start_time > 1.0))
        {
            gametext.text = " ";
        }

        if (GameOverFlag.End_flag == true)
        {
            gametext.text = "GAME OVER";
        }
    }
}
