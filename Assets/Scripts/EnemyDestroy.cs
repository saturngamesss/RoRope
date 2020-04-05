//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    [Header ("GameObjects")]
    public GameObject Enemy;
    public GameObject ShootingParticle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            GameObject colObject = other.gameObject;

            GameObject ShootingParticleClone = Instantiate(ShootingParticle);
            ShootingParticleClone.transform.position = new Vector3(transform.position.x + 1, transform.position.y, -10);

            Destroy(ShootingParticleClone, 3);
            Destroy(Enemy);
            Destroy(colObject);
            Destroy(gameObject);

            FindObjectOfType<AudioManager>().Play("Explosion");
        }
    }
}
