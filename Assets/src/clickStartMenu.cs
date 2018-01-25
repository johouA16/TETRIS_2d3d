using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickStartMenu : MonoBehaviour {

    // クリックした位置座標
    private Vector3 p;

    public void start3dGame()
    {
        Debug.Log("3D game start");
        SceneManager.LoadScene("3d");
    }

    public void start2dGame()
    {
        Debug.Log("2D game start");
        SceneManager.LoadScene("2d");
    }

    // Update is called once per frame
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            start2dGame();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            start3dGame();
        }

    }
}
