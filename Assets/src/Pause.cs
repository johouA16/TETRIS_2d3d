using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public static bool stopTime = false;
    public static GameObject obj;
    public static GameObject returnBtn;

    // Use this for initialization
    void Start()
    {
        //gameObject.SetActive(false);
        obj = gameObject;
        obj.SetActive(false);
        Time.timeScale = 1;
        stopTime = false;
    }

    public static void GameStop()
    {
        if (!stopTime)
        {
            obj.SetActive(true);
            Time.timeScale = 0;
            stopTime = true;
        }
        else
        {
            obj.SetActive(false);
            Time.timeScale = 1;
            stopTime = false;
        }
    }
}
