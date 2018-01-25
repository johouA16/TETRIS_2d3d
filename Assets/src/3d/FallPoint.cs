using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPoint : MonoBehaviour
{

    public GameObject fallPoint;
    public static List<Vector3> fallPosition = new List<Vector3>();
    public static List<GameObject> fpList = new List<GameObject>();

    public static bool isUpdateFallpoint = false;

    public static void setFallPoint(Transform tf)
    {
        resetFallPoint();

        foreach (Transform child in tf)
        {
            Vector3 v = Grid3d.roundVec3(child.position);


            if ((int)v.y != 0 && Grid3d.grid[(int)v.x, (int)v.y - 1, (int)v.z] == null)
            {
                fallPosition.Add(calcPutFallPoint(child.position));
            }
        }

        isUpdateFallpoint = false;
    }

    public static void resetFallPoint()
    {
        foreach (GameObject fp in fpList)
        {
            Destroy(fp);
        }
        fpList.Clear();

        fallPosition = new List<Vector3>();
    }

    public static Vector3 calcPutFallPoint(Vector3 v)
    {
        while (true)
        {
            v.y--;
            if (Grid3d.grid[(int)v.x, (int)v.y, (int)v.z] != null)
            {
                v.y++;
                return v;
            }

            if ((int)v.y == 0)
            {
                return v;
            }
        }
    }

    void putFallPoint(Vector3 _v)
    {
        GameObject fp = Object.Instantiate(fallPoint) as GameObject;

        Vector3 v = new Vector3(_v.x, (float)(_v.y - 0.5), _v.z);
        fp.transform.position = v;
        fpList.Add(fp);

    }

    void putFallPoints(List<Vector3> vec)
    {
        foreach (Vector3 v in vec)
        {
            putFallPoint(v);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdateFallpoint == false)
        {
            putFallPoints(fallPosition);
            isUpdateFallpoint = true;
        }
    }
}
