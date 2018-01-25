using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCameraRoute3d : MonoBehaviour
{


    bool RotationFlag = false;


    float[] Angle = { 0.0F, 90.0F, 180.0F, -90.0F };

    double startTime;
    double time = 0.7;

    int NowPos = 0;
    int NextPos = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && RotationFlag == false)
        {
            NextPos = NowPos + 1;
            if (NowPos == 3)
            {
                NextPos = 0;
            }

            RotationFlag = true;
            startTime = Time.timeSinceLevelLoad;
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && RotationFlag == false)
        {
            NextPos = NowPos - 1;
            if (NowPos == 0)
            {
                NextPos = 3;
            }

            RotationFlag = true;
            startTime = Time.timeSinceLevelLoad;

        }

        if (RotationFlag)
        {
            double diff = Time.timeSinceLevelLoad - startTime;
            float rate = (float)diff / (float)time;

            if (diff > time)
            {
                RotationFlag = false;
                NowPos = NextPos;
            }

            float angle = Mathf.LerpAngle(Angle[NowPos], Angle[NextPos], rate);
            transform.eulerAngles = new Vector3(90, angle, 0);

        }
    }
}
