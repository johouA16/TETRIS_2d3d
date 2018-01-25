using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    //点数を格納する変数
    public static int Score = 0;

    //地面に設置したブロックの総数
    public static int FallBlocksNum = 0;

    //FallBlocksNumをインクリメントし、今までの総数を返す
    public static int addFallBlocks()
    {
        FallBlocksNum++;
        //Debug.Log("sum block ->" + FallBlocksNum.ToString());

        return FallBlocksNum;
    }

    public static void addScore(int score)
    {
        Score += score;
        //Debug.Log("add Score ->" + score.ToString());
    }

    void Start()
    {
        Score = 0;
        FallBlocksNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "SCORE: " + Score.ToString();
    }
}
