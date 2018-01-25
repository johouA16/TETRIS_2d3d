using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameBtn : MonoBehaviour {

    public static GameObject finish_btn;

    // Use this for initialization
    void Start()
    {
        finish_btn = gameObject;
        finish_btn.SetActive(false);
    }

    public static void displayFinishBtn()
    {
        finish_btn.SetActive(true);
    }


    public void OnClick()
    {
        Debug.Log("On click Finish");

        SceneManager.LoadScene("StartMenu");
    }
}
