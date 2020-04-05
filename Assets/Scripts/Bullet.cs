//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header ("Particle")]
    public GameObject BulletColParticle;

    public LayerMask layer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameObject BulletColParticleClone = Instantiate(BulletColParticle);
            BulletColParticleClone.transform.position = new Vector3(transform.position.x + 1, transform.position.y, -10);

            Destroy(BulletColParticleClone, 2f);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Swing"))
        {
            GameObject BulletColParticleClone = Instantiate(BulletColParticle);
            BulletColParticleClone.transform.position = new Vector3(transform.position.x + 1, transform.position.y, -10);

            Destroy(BulletColParticleClone, 2f);
            Destroy(gameObject);
        }

    }

}
