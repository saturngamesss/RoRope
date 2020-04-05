//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class SwingBlock : MonoBehaviour
{
    [HideInInspector]
    public bool cursorIsTrig;

    private void OnMouseEnter()
    {
        cursorIsTrig = true;
    }

    private void OnMouseExit()
    {
        cursorIsTrig = false;
    }
}
