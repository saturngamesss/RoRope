//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;
using System.Collections.Generic;

public class PlayerAI : MonoBehaviour
{
    [Header ("Other")]
    public Animator animator;
    public Rigidbody2D rb;
    public GroundChecker gc;

    Vector2 lookDirection;

    [Header ("Transforms")]
    public Transform groundCheckPoint;
    public Transform firePoint;

    [Header("Floats")]
    public float speed = 30;
    public float jumpForce = 10.0f;
    public float reloadTime = 0.5f;
    private float lookAngle;

    [Header("GameObjects")]
    public GameObject gun;
    public GameObject bullet;
    public GameObject ShootingParticle;
    public GameObject GroundColParticle;

    public List <GameObject> players = new List <GameObject>();

    GameObject trigGun;


    [Header("Sensor")]
    public float JumpSensorLenght = 5.0f;
    public Transform castPointJump;
    Vector2 jumpEndPos;
    Vector2 shootingEndPos;


    [HideInInspector]
    public bool isGrounded, isTrigGun = false, isTrigGrapplingGun = false, fellOnGround, isJumping, isColWall, Checker = true, sensorChecker;

    void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.Equals(this.gameObject))
                continue;
            players.Add(player);
        }
    }

    void FixedUpdate()
    {
        isGrounded = gc.isColGround;
        reloadTime -= Time.deltaTime;

        lookDirection = players[0].transform.position - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        gun.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        JumpSensor(JumpSensorLenght);
        ShootingSensor();

        if (gun.activeSelf == false)
        {
            if (JumpSensor(JumpSensorLenght))
            {
                Jump();
            }
            else
            {
                GameObject gun;

                if (GameObject.FindGameObjectWithTag("Gun"))
                {
                    gun = GameObject.FindGameObjectWithTag("Gun");

                    if (gun.transform.position.x > transform.position.x)
                    {
                        MoveRight();
                    }
                    else if (gun.transform.position.x < transform.position.x)
                    {
                        MoveLeft();
                    }
                }
                else
                {
                    Panic();
                }
            }
        }

        if (gun.activeSelf == true)
        {
            if (sensorChecker == false)
            {
                if (JumpSensor(JumpSensorLenght))
                {
                    Jump();
                }
                else
                {
                    if (gun.transform.position.x > transform.position.x)
                    {
                        MoveRight();
                    }
                    else if (gun.transform.position.x < transform.position.x)
                    {
                        MoveLeft();
                    }
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (reloadTime <= 0 && gun.activeSelf == true)
        {
            Shoot();
            reloadTime = 1.5f;
        }

        if (isColWall == true)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            isTrigGun = true;
            trigGun = other.gameObject;

            PickUpGun();
        }

        if (other.gameObject.CompareTag("EnemyBullet") && Checker == true)
        {
            Checker = false;
            Destroy(gameObject);
        }
    }

    bool JumpSensor(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (rb.velocity.x > 0)
        {
            jumpEndPos = castPointJump.position + Vector3.right * distance;
        }
        else if (rb.velocity.x < 0)
        {
            jumpEndPos = castPointJump.position + -Vector3.right * distance;
        }

        Debug.Log(jumpEndPos);
        
        RaycastHit2D hit = Physics2D.Linecast(castPointJump.position, jumpEndPos, 1 << LayerMask.NameToLayer("Ground"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(castPointJump.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(castPointJump.position, jumpEndPos, Color.blue);
        }

        return val;
    }

    bool ShootingSensor()
    {
        bool val = true;
        sensorChecker = val;

        shootingEndPos = players[0].transform.position;

        RaycastHit2D hit = Physics2D.Linecast(firePoint.position, shootingEndPos, 1 << LayerMask.NameToLayer("Ground"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
                sensorChecker = val;
                Debug.Log(sensorChecker);
            }
            else
            {
                val = false;
                sensorChecker = val;
                Debug.Log(sensorChecker);
            }

            Debug.DrawLine(castPointJump.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(castPointJump.position, shootingEndPos, Color.blue);
        }
        
        return val;
    }

    public void PickUpGun()
    {
        if (isTrigGun == true && gun.activeSelf == false)
        {
            gun.SetActive(true);
            Destroy(trigGun);

            FindObjectOfType<AudioManager>().Play("PickUp");
        }

        Debug.Log("PickupGun");
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        animator.SetBool("Flip", true);

        Debug.Log("MoveLeft");
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        animator.SetBool("Flip", false);

        Debug.Log("MoveRight");
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            fellOnGround = false;
            isJumping = true;

            FindObjectOfType<AudioManager>().Play("Jump");

            Debug.Log("Jump");
        }
        else if (isGrounded == true && fellOnGround == false)
        {
            GameObject GroundParticleClone = Instantiate(GroundColParticle);
            GroundParticleClone.transform.position = new Vector3(groundCheckPoint.transform.position.x, groundCheckPoint.transform.position.y, -10);
            Destroy(GroundParticleClone, 5);

            fellOnGround = true;
        }
    } 

    public void Shoot()
    {
        if (gun.activeSelf == true)
        {
            GameObject BulletClone = Instantiate(bullet);
            BulletClone.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
            BulletClone.transform.position = firePoint.position;
            BulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right* 200f;
            Destroy(BulletClone, 5);

            GameObject ShootingParticleClone = Instantiate(ShootingParticle);
            ShootingParticleClone.transform.position = new Vector3(firePoint.position.x + 1, firePoint.position.y, -10);
            Destroy(ShootingParticleClone, 5);

            Debug.Log("Shot");

            FindObjectOfType<AudioManager>().Play("GunShot");
        }
    }

    public void Panic()
    {
        Jump();

        if (players[0].transform.position.x > transform.position.x)
        {
            MoveLeft();
        }
        else if (players[0].transform.position.x < transform.position.x)
        {
            MoveRight();
        }

    }
}
