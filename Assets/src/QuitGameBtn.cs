using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameBtn : MonoBehaviour {

    public void OnClick()
    {
        SceneManager.LoadScene("StartMenu");
        Debug.Log("On click Quit");
    }
}
