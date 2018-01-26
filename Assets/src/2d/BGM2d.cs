using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM2d : MonoBehaviour {

    AudioSource audiosource;    

    // Use this for initialization
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
    }

	// Update is called once per frame
	void Update () {
       if (GameOverFlag.End_flag == true)
        {
            audiosource.Stop();
        }
    }
}
