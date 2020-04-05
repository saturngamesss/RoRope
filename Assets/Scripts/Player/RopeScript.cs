//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class RopeScript : MonoBehaviour
{
    [Header ("Scripts")]
    public PlayerMovement pm;
    public SwingBlock swingBlock;

    [Header ("Other")]
    public LineRenderer lineRenderer;
    public GameObject ropeShooter;

    private DistanceJoint2D rope;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && swingBlock.cursorIsTrig == true && pm.grapplingGun.activeSelf == true)
        {         
            Fire();

            FindObjectOfType<AudioManager>().Play("Rope");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            GameObject.DestroyImmediate(rope);
            lineRenderer.enabled = false;

            pm.enabled = true;
        }
    }

    void LateUpdate()
    {
        if (rope != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, ropeShooter.transform.position);
            lineRenderer.SetPosition(1, rope.connectedAnchor);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void Fire()
    {
        pm.enabled = false;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = ropeShooter.transform.position;
        Vector3 direction = mousePosition - position;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

        if (hit.collider != null)
        {
            DistanceJoint2D newRope = ropeShooter.AddComponent<DistanceJoint2D>();
            newRope.enableCollision = false;
            newRope.connectedAnchor = mousePosition;
            newRope.enableCollision = true;
            newRope.enabled = true;

            Destroy(rope);
            rope = newRope;
        }
    }
}