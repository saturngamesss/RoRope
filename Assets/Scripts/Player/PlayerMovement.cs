//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//realgamesss.weebly.com
//gamejolt.com/@Real_Game
//realgamesss.newgrounds.com/
//real-games.itch.io/
//youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Other")]
    public Animator animator;
    public UI ui;
    public GroundChecker gc;
    public Transform groundCheckPoint;
    public Transform firePoint;

    Vector2 lookDirection;
    Rigidbody2D rb;

    [Header ("GameObjects")]
    public GameObject gun;
    public GameObject grapplingGun;
    public GameObject bullet;
    public GameObject spanwedGun;
    public GameObject spawnedGrapplingGun;
    public GameObject ShootingParticle;
    public GameObject PowerupParticle;
    public GameObject GroundColParticle;

    GameObject trigGun;
    GameObject trigGrapplingGun;

    [Header ("Floats")]
    public float speed = 30;
    public float jumpForce = 5;
    public float reloadTime = 0.2f;
    public float groundCheckRadius;

    float lookAngle;

    [HideInInspector]
    public bool isGrounded, isTrigGun = false, isTrigGrapplingGun = false, fellOnGround, isJumping, isColWall, Checker = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        reloadTime -= Time.deltaTime;

        isGrounded = gc.isColGround;

        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        gun.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        grapplingGun.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        // PickUp Gun
        if (isTrigGun == true && Input.GetKeyDown(KeyCode.E) && gun.activeSelf == false && grapplingGun.activeSelf == false)
        {
            gun.SetActive(true);
            Destroy(trigGun);

            ui.Gun.SetActive(true);
            ui.GrapplingGun.SetActive(false);

            FindObjectOfType<AudioManager>().Play("PickUp");
        }
        // Throw Gun
        else if (gun.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            GameObject gunClone = Instantiate(spanwedGun, firePoint.position, firePoint.rotation);
            gun.SetActive(false);

            ui.Gun.SetActive(false);
        }

        // PickUp Grappling Gun
        if (isTrigGrapplingGun == true && Input.GetKeyDown(KeyCode.E) && gun.activeSelf == false && grapplingGun.activeSelf == false)
        {
            grapplingGun.SetActive(true); 
            Destroy(trigGrapplingGun);

            ui.Gun.SetActive(false);
            ui.GrapplingGun.SetActive(true);

            FindObjectOfType<AudioManager>().Play("PickUp");
        }
        // Throw Grappling Gun
        else if (grapplingGun.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            GameObject gunClone = Instantiate(spawnedGrapplingGun, firePoint.position, firePoint.rotation);
            grapplingGun.SetActive(false);

            ui.GrapplingGun.SetActive(false);
        }

        // Move Controller
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        // Flip Player
        if (rb.velocity.x < 0)
        {
            animator.SetBool("Flip", true);
        }
        else if (rb.velocity.x > 0 )
        {
            animator.SetBool("Flip", false);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && ui.PauseMenu.activeSelf == false && ui.GameOverMenu.activeSelf == false && ui.VictoryMenu.activeSelf == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            fellOnGround = false;
            isJumping = true;

            FindObjectOfType<AudioManager>().Play("Jump");
        }

        // Double Jump
        else if (Input.GetKeyDown(KeyCode.Space) && isJumping == true )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            GameObject GroundParticleClone = Instantiate(GroundColParticle);
            GroundParticleClone.transform.position = new Vector3(groundCheckPoint.transform.position.x, groundCheckPoint.transform.position.y, -10);
            Destroy(GroundParticleClone, 5);

            if (rb.velocity.x > 0)
            {
                animator.SetBool("RBackflip", true);
                StartCoroutine(RBackflip());
            }   
            else if (rb.velocity.x < 0)
            {
                animator.SetBool("LBackflip", true);
                StartCoroutine(LBackflip());
            }
            
            isJumping = false;

            FindObjectOfType<AudioManager>().Play("Jump");
        }

        // Jump Efect
        else if (isGrounded == true && fellOnGround == false)
        {
            GameObject GroundParticleClone = Instantiate(GroundColParticle);
            GroundParticleClone.transform.position = new Vector3(groundCheckPoint.transform.position.x, groundCheckPoint.transform.position.y, -10);
            Destroy(GroundParticleClone, 5);

            fellOnGround = true;
        }

        // Shooting Bullets
        if (Input.GetMouseButtonDown(0) && gun.activeSelf == true && ui.PauseMenu.activeSelf == false && ui.GameOverMenu.activeSelf == false && ui.VictoryMenu.activeSelf == false )
        {
            GameObject BulletClone = Instantiate(bullet);
            BulletClone.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
            BulletClone.transform.position = firePoint.position;
            BulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * 200f;
            Destroy(BulletClone, 5);

            GameObject ShootingParticleClone = Instantiate(ShootingParticle);
            ShootingParticleClone.transform.position = new Vector3(firePoint.position.x + 1, firePoint.position.y, -10);
            Destroy(ShootingParticleClone, 5);

            reloadTime = 1;

            FindObjectOfType<AudioManager>().Play("GunShot");
        }
        
        if (isColWall == true)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            isTrigGun = true;
            trigGun = other.gameObject;
        }

        if (other.gameObject.CompareTag("GrapplingGun"))
        {
            isTrigGrapplingGun = true;
            trigGrapplingGun = other.gameObject;
        }

        if (other.gameObject.CompareTag("EnemyBullet") && Checker == true)
        {
            Checker = false;
            ui.GameOverMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (other.gameObject.CompareTag("JumpBoost"))
        {
            jumpForce += 15;

            GameObject ShootingParticleClone = Instantiate(PowerupParticle);
            ShootingParticleClone.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, -10);
            Destroy(ShootingParticleClone, 5);

            Destroy(other.gameObject);

            FindObjectOfType<AudioManager>().Play("PowerUp");

            ui.JumpBoost.SetActive(true);
            StartCoroutine(jumpBoost());
        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            speed += 30;

            GameObject ShootingParticleClone = Instantiate(PowerupParticle);
            ShootingParticleClone.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, -10);
            Destroy(ShootingParticleClone, 5);

            Destroy(other.gameObject);

            FindObjectOfType<AudioManager>().Play("PowerUp");

            ui.SpeedBoost.SetActive(true);
            StartCoroutine(speedBoost());
        }

        if (other.gameObject.CompareTag("Coin") && Checker == true)
        {
            Checker = false;
            ui.VictoryMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gun"))
        {
            isTrigGun = false;
        }

        if (other.gameObject.CompareTag("GrapplingGun"))
        {
            isTrigGrapplingGun = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") && Checker == true)
        {
            Checker = false;
            ui.GameOverMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }      
    }

    IEnumerator jumpBoost()
    {
        yield return new WaitForSeconds(5);

        ui.JumpBoost.SetActive(false);
        jumpForce -= 15;
    }

    IEnumerator speedBoost()
    {
        yield return new WaitForSeconds(5);

        ui.SpeedBoost.SetActive(false);
        speed -= 30; 
    }

    IEnumerator RBackflip()
    {
        yield return new WaitForSeconds(1);

        animator.SetBool("RBackflip", false);
    }

    IEnumerator LBackflip()
    {
        yield return new WaitForSeconds(1);

        animator.SetBool("LBackflip", false);
    }
}