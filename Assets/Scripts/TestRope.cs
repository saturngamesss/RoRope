//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRope : MonoBehaviour
{
    DistanceJoint2D rope;
    LineRenderer lr;

    bool checker;

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);

        if (Input.GetMouseButtonDown(0) && checker == true)
        { 
            rope = gameObject.AddComponent<DistanceJoint2D>();
            rope.connectedAnchor = mousePos;

            lr.enabled = true;
            lr.SetPosition(1, rope.connectedAnchor);

            checker = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            lr.enabled = false;
            DestroyImmediate(rope);

            checker = true;
        }
    }
}
