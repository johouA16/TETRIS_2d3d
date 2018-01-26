using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3d : MonoBehaviour
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
            current.transform.position = new Vector3(3, 10, 3);//スポーンポイント


            //nextミノの決定・生成
            nextID = Random.Range(0, groups.Length);
            next = CreateMino(nextID);
            next.transform.position = new Vector3(-20, 50, 5);
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
                current.transform.position = new Vector3(3, 10, 3); //スポーンポイント
                GameObject.Destroy(hold);
            }

            hold = CreateMino(currID);
            spwaudio = GetComponent<AudioSource>();
            hold.transform.position = new Vector3(-20, 40, 5);
            holdID = currID;

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
