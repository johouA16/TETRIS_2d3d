using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group2d : MonoBehaviour
{
    // Time since last gravity tick
    float lastFall = 0;
    int fallThreshNum = 20;
    float underFallVel = (float)0.7;

    public static double fallTime = 1.0;

    public AudioClip fallSE;
    AudioSource audiosource;

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid2d.roundVec2(child.position);

            // Not inside Border?
            if (!Grid2d.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Grid2d.grid[(int)v.x, (int)v.y] != null &&
                Grid2d.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Grid2d.h; ++y)
            for (int x = 0; x < Grid2d.w; ++x)
                if (Grid2d.grid[x, y] != null)
                    if (Grid2d.grid[x, y].parent == transform)
                        Grid2d.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Grid2d.roundVec2(child.position);
            Grid2d.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void fall_sequence()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update Grid2d.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update Grid2d.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // See if valid
            if (isValidGridPos())
                updateGrid();
            else
            {
                transform.position += new Vector3(-1, 0, 0);
                if (isValidGridPos())
                    updateGrid();
                else
                {
                    transform.position += new Vector3(-1, 0, 0);
                    if (isValidGridPos())
                        updateGrid();
                    else
                    {

                        transform.position += new Vector3(3, 0, 0);
                        if (isValidGridPos())
                            updateGrid();
                        else
                        {
                            transform.position += new Vector3(1, 0, 0);
                            if (isValidGridPos())
                                updateGrid();
                            else
                            {
                                // It's not valid. revert.
                                transform.Rotate(0, 0, 90);
                                transform.position += new Vector3(-2, 0, 0);
                            }
                        }
                    }
                }
            }
        }
        // Move Downwards and Fall
        else if ((Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall >= 0.1) ||
                 Time.time - lastFall >= fallTime)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update Grid2d.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                audiosource.PlayOneShot(fallSE);
                Spawner2d.second_ban = false;

                // Clear filled horizontal lines
                Grid2d.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner2d>().spawnNext();

                // 落ちるのが早くなるところ
                if (ScoreText.addFallBlocks() % fallThreshNum == 0)
                {
                    if(fallTime > underFallVel)
                    {
                        fallTime -= 0.1;
                    }
                   
                    Debug.Log("fallTime ->" + fallTime.ToString());
                }

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
        // Fall
        else if (Input.GetKeyUp(KeyCode.Space))
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

            audiosource.PlayOneShot(fallSE);
            Spawner2d.second_ban = false;

            //スコア加算
            ScoreText.addScore(score);

            // Clear filled horizontal lines
            Grid2d.deleteFullRows();

            // Spawn next Group
            FindObjectOfType<Spawner2d>().spawnNext();

            // 落ちるのが早くなるところ
            if (ScoreText.addFallBlocks() % fallThreshNum == 0)
            {
                if (fallTime > underFallVel)
                {
                    fallTime -= 0.1;
                }
                Debug.Log("fallTime ->" + fallTime.ToString());
            }

            // Disable script
            enabled = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (0 <= transform.position.x && transform.position.x <= 9)
        {
            if (!isValidGridPos())
            {
                Debug.Log("GAME OVER");
                Destroy(gameObject);
                GameOverFlag.End_flag = true;
                FinishGameBtn.displayFinishBtn();
            }
        }
        audiosource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.x);

        if (0 <= transform.position.x && transform.position.x <= 9)
        {
            if (Time.timeScale > 0)
            {
                fall_sequence();

                //Hold
                if (Input.GetKeyDown(KeyCode.H) && Spawner2d.second_ban == false)
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
