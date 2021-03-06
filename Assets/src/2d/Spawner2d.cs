﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2d : MonoBehaviour
{
    // Groups
    public GameObject[] groups;

    public AudioClip holdSE;
    AudioSource spwaudio;

    public static bool hold_flag = false;
    public static bool second_ban = false;

    GameObject current;
    GameObject next;
    GameObject hold;
    int currID, nextID, holdID;

    bool FirstSpawn = true;

    //ミノをborder内に移動（スポーン）
    public void spawnNext()
    {
        if (GameOverFlag.End_flag == false)
        {
            //Debug.Log("called");
            //落下ミノの決定
            if (FirstSpawn)
            {
                currID = Random.Range(0, groups.Length);
                FirstSpawn = false;
            }
            else
            {
                currID = nextID;
                Destroy(next);
            }

            //落下ミノの生成
            current = CreateMino(currID);
            current.transform.position = new Vector3(5, 13, 0);


            //nextミノの決定・生成
            nextID = Random.Range(0, groups.Length);
            next = CreateMino(nextID);
            next.transform.position = new Vector3(14, 9, 0);

        }
    }

    public GameObject CreateMino(int MinoID)
    {
        return Instantiate(groups[MinoID],
                           transform.position,
                           Quaternion.identity);
    }


    // Use this for initialization
    void Start()
    {
        Debug.Log("call");
        GameOverFlag.End_flag = false;
        spwaudio = GetComponent<AudioSource>();
        //Group2d.fallTime = 1.0;
        spawnNext();
    }

    //Hold機能該当部分
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.H) && second_ban == false && GameOverFlag.End_flag == false)
        {          
          
            if (hold_flag == true)
            {
                current = CreateMino(holdID);
                current.transform.position = new Vector3(5, 13, 0);
                GameObject.Destroy(hold);
            }

            hold = CreateMino(currID);
            hold.transform.position = new Vector3(14, 2, 0);
            holdID = currID;


            //初回だけ
            if (hold_flag == false)
            {
                spawnNext();
                hold_flag = true;
            }
            spwaudio.PlayOneShot(holdSE);
            second_ban = true;
        }
    }
}
