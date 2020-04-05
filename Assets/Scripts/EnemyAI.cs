//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header ("Other")]
    public GameObject bullet;
    public float reload = 1;
    public EnemyTrig enemyTrig;

    GameObject player;
    float lookAngle;
    Vector2 lookDirection;
   
    [Header ("Transforms")]
    public Transform firePoint;
    public Transform projectile;
    public Transform eyeL, eyeR;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        reload -= Time.deltaTime;

        lookDirection = projectile.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        Vector2 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion qto = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, qto, 5f * Time.deltaTime);
        eyeL.transform.rotation = Quaternion.Slerp(eyeL.transform.rotation, qto, 5f * Time.deltaTime);
        eyeR.transform.rotation = Quaternion.Slerp(eyeR.transform.rotation, qto, 5f * Time.deltaTime);

        if (reload < 0 && enemyTrig.isTrigPlayer == true)
        {
            GameObject firedBullet = Instantiate(bullet);
            firedBullet.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
            firedBullet.transform.position = firePoint.position;
            firedBullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * 100f;

            Destroy(firedBullet, 5);
            reload = 1;

            FindObjectOfType<AudioManager>().Play("GunShot");
        }
    }
}
