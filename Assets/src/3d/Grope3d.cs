using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grope3d : MonoBehaviour
{

    // Time since last gravity tick
    float lastFall = 0;

    bool isValidGridPos()
    {

        foreach (Transform child in transform)
        {
            Vector3 v = Grid3d.roundVec3(child.position);

            // Not inside Border?
            if (!Grid3d.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?

            if (Grid3d.grid[(int)v.x, (int)v.y, (int)v.z] != null &&
                Grid3d.grid[(int)v.x, (int)v.y, (int)v.z].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid3d.h; ++y)
            for (int x = 0; x < Grid3d.w; ++x)
                for (int z = 0; z < Grid3d.d; ++z)
                    if (Grid3d.grid[x, y, z] != null)
                        if (Grid3d.grid[x, y, z].parent == transform)
                            Grid3d.grid[x, y, z] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector3 v = Grid3d.roundVec3(child.position);
            Grid3d.grid[(int)v.x, (int)v.y, (int)v.z] = child;
        }
    }

    // Use this for initialization
    void Start()
    {

        if (0 <= transform.position.x && transform.position.x <= 6 &&
            0 <= transform.position.z && transform.position.z <= 6 &&
            0 <= transform.position.y && transform.position.y <= 15)
        {
            // Default position not valid? Then it's game over
            if (!isValidGridPos())
            {
                Debug.Log("GAME OVER");
                Destroy(gameObject);
                GameOverFlag.End_flag = true;
                FinishGameBtn.displayFinishBtn();
            }

            FallPoint.setFallPoint(transform);
        }
    }

    //前に進めるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveFront()
    {
        // Modify position
        transform.position += new Vector3(0, 0, 1);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(0, 0, -1);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //後ろに進めるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveBack()
    {
        // Modify position
        transform.position += new Vector3(0, 0, -1);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(0, 0, 1);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //左に動けるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveLeft()
    {
        // Modify position
        transform.position += new Vector3(-1, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(1, 0, 0);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //右に動けるかチェック
    //チェックして進める場合、移動も行う
    void checkMoveRight()
    {
        // Modify position
        transform.position += new Vector3(1, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.position += new Vector3(-1, 0, 0);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //ミノ回転時に壁際での回転対策用
    bool checkRotationPosition()
    {

        //Debug.Log(transform.rotation.x.ToString() + " " + transform.rotation.x.ToString() + " " + transform.rotation.z.ToString());

        if (isValidGridPos())
            return true;
        //1
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPos())
            return true;
        //2
        transform.position += new Vector3(0, 0, -1);
        if (isValidGridPos())
            return true;
        //3
        transform.position += new Vector3(1, 0, 0);
        if (isValidGridPos())
            return true;
        //4
        transform.position += new Vector3(1, 0, 1);
        if (isValidGridPos())
            return true;
        //5
        transform.position += new Vector3(0, 0, 1);
        if (isValidGridPos())
            return true;
        //6
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPos())
            return true;
        //7
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPos())
            return true;
        //8
        transform.position += new Vector3(-1, 0, 0);
        if (isValidGridPos())
            return true;
        //9
        transform.position += new Vector3(0, 0, -1);
        if (isValidGridPos())
            return true;
        //10
        transform.position += new Vector3(0, 0, -1);
        if (isValidGridPos())
            return true;
        //11
        transform.position += new Vector3(0, 0, -1);
        if (isValidGridPos())
            return true;
        //12
        transform.position += new Vector3(1, 0, 0);
        if (isValidGridPos())
            return true;
        //13
        transform.position += new Vector3(1, 0, 0);
        if (isValidGridPos())
            return true;
        //14
        transform.position += new Vector3(1, 0, 0);
        if (isValidGridPos())
            return true;
        //15
        transform.position += new Vector3(0, 0, 1);
        if (isValidGridPos())
            return true;
        //16
        //元の位置へ
        transform.position += new Vector3(-1, 0, 1);

        //一段上をチェック
        transform.position += new Vector3(0, 1, 0);
        if (checkRotationPosition())
            return true;


        //元の位置へ
        transform.position += new Vector3(0, -1, 0);

        return false;
    }

    //x軸回転できるかチェック
    void checkRotateXaxis(bool dir)
    {
        if(dir)
            transform.Rotate(-90, 0, 0, Space.World);
        else
            transform.Rotate(90, 0, 0, Space.World);

        // See if valid
        // if (isValidGridPos())
        if (checkRotationPosition())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            if (dir)
                transform.Rotate(90, 0, 0, Space.World);
            else
                transform.Rotate(-90, 0, 0, Space.World);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //y軸回転できるかチェック
    void checkRotateYaxis()
    {
        transform.Rotate(0, -90, 0, Space.World);

        // See if valid
        // if (isValidGridPos())
        if (checkRotationPosition())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            transform.Rotate(0, 90, 0, Space.World);

        //影を設置
        FallPoint.setFallPoint(transform);
    }

    //z軸回転できるかチェック
    void checkRotateZaxis(bool dir)
    {
        if(dir)
            transform.Rotate(0, 0, -90, Space.World);
        else
            transform.Rotate(0, 0, 90, Space.World);

        // See if valid
        // if (isValidGridPos())
        if (checkRotationPosition())
            // It's valid. Update grid.
            updateGrid();
        else
        // It's not valid. revert.
            if (dir)
                transform.Rotate(0, 0, 90, Space.World);
            else
                transform.Rotate(0, 0, -90, Space.World);

        //影を設置
        FallPoint.setFallPoint(transform);
    }


    // Update is called once per frame
    void fall_sequence()
    {

        switch (CameraMove3d.getPositionNumber())
        {
            case 0:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveFront();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveBack();
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveLeft();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveRight();
                }


                // Rotate
                if (Input.GetKeyDown(KeyCode.A))
                {
                    checkRotateXaxis(true);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    checkRotateYaxis();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    checkRotateZaxis(true);
                }
                break;

            case 1:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveRight();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveLeft();
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveFront();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveBack();
                }


                // Rotate
                if (Input.GetKeyDown(KeyCode.D))
                {
                    checkRotateXaxis(true);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    checkRotateYaxis();
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    checkRotateZaxis(true);
                }
                break;

            case 2:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveBack();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveFront();
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveRight();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveLeft();
                }


                // Rotate
                if (Input.GetKeyDown(KeyCode.A))
                {
                    checkRotateXaxis(false);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    checkRotateYaxis();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    checkRotateZaxis(false);
                }
                break;

            case 3:
                // Move Front
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    checkMoveLeft();
                }

                // Move Back
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    checkMoveRight();
                }

                // Move Left
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    checkMoveBack();
                }

                // Move Right
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    checkMoveFront();
                }


                // Rotate
                if (Input.GetKeyDown(KeyCode.D))
                {
                    checkRotateXaxis(false);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    checkRotateYaxis();
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    checkRotateZaxis(false);
                }
                break;
        }



        //時間経過で落ちる
        if (Time.time - lastFall >= 1.5)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid3d.deleteFullRows();

                Spawner3d.second_ban = false;

                // Spawn next Group
                FindObjectOfType<Spawner3d>().spawnNext();

                //影を消す
                FallPoint.resetFallPoint();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;

        }

        //一気に落ちる
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            int score = 0;

            while (true)
            {

                transform.position += new Vector3(0, -1, 0);

                if (!isValidGridPos())
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);
                    break;
                }
                score++;

            }
            updateGrid();

            //スコア加算
            ScoreText.addScore(score);

            // Clear filled horizontal lines
            Grid3d.deleteFullRows();

            Spawner3d.second_ban = false;

            // Spawn next Group
            FindObjectOfType<Spawner3d>().spawnNext();

            //影を消す
            FallPoint.resetFallPoint();

            // Disable script
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.x);
        //fall_sequence();

        if (0 <= transform.position.x && transform.position.x <= 6 && 
            0 <= transform.position.z && transform.position.z <= 6 &&
            0 <= transform.position.y && transform.position.y <= 12)
        {
            if (Time.timeScale > 0)
            {
                fall_sequence();

                //Hold
                if (Input.GetKeyDown(KeyCode.H) && Spawner3d.second_ban == false)
                {
                    GameObject.Destroy(gameObject);
                }
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Pause.GameStop();
            }
        }
    }

}
