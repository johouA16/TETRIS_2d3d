using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnGameBtn : MonoBehaviour {


    public void OnClick()
    {
        Pause.GameStop();
        Debug.Log("On click Return");
    }
}
