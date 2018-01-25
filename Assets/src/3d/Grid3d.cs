using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3d : MonoBehaviour
{

    // The Grid itself
    public static int w = 6;
    public static int h = 20;
    public static int d = 6;
    public static Transform[,,] grid = new Transform[w, h, d];

    //四捨五入(正確には偶数丸め)
    public static Vector3 roundVec3(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x),
                           Mathf.Round(v.y),
                           Mathf.Round(v.z));
    }

    public static bool insideBorder(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w &&
                (int)pos.z >= 0 && (int)pos.z < d &&
                (int)pos.y >= 0);
    }


    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            for (int z = 0; z < d; ++z)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            for (int z = 0; z < d; ++z)
            {
                if (grid[x, y, z] != null)
                {
                    // Move one towards bottom
                    grid[x, y - 1, z] = grid[x, y, z];
                    grid[x, y, z] = null;

                    // Update Block position
                    grid[x, y - 1, z].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            for (int z = 0; z < d; ++z)
                if (grid[x, y, z] == null)
                    return false;

        return true;
    }

    public static void deleteFullRows()
    {
        int num = 0;
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                num++;
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }

        switch (num)
        {
            case 1:
                ScoreText.addScore(40 * 6 * 6);
                break;
            case 2:
                ScoreText.addScore(100 * 6 * 6);
                break;
            case 3:
                ScoreText.addScore(300 * 6 * 6);
                break;
            case 4:
                ScoreText.addScore(1200 * 6 * 6);
                break;
            default:

                break;
        }
    }

}
