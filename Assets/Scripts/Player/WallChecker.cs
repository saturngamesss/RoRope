//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public PlayerMovement pm;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            pm.isColWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        pm.isColWall = false;
    }
}
