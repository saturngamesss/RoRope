//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [HideInInspector]
    public bool isColGround;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColGround = false;
        }
    }
}
