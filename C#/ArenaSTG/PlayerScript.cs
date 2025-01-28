//Niko Väänänen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Initialize a number of values for the player and some other stuff
    private Rigidbody2D rb2d;
    private Camera mainCamera;
    private PlayerShoot ShootScript;
    SpriteRenderer playerSpriteRenderer;
    [SerializeField] private int score;
    [SerializeField][Range(1.0f, 10.0f)] private float scoreMultiplier;
    [SerializeField] private int coins;
    [SerializeField] private int speed;
    [SerializeField][Range(0, 5)] private int bombStock;
    [SerializeField][Range(0, 5)] private int hp;
    [SerializeField] private bool collect;
    [SerializeField] private bool shield;
    [SerializeField] private int shieldRecharge;
    [SerializeField] private int shieldRechargePenalty;
    [SerializeField] private int turboMultiplier;
    [SerializeField] private int turboLevel;
    [SerializeField] private int bombTimer;
    [SerializeField] private int turboTimer;
    private int turboCooldownTimer;
    private int hitInvulTimer;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private bool shootLeft;
    private bool shootRight;
    private bool pause;
    private bool bomb;
    private bool turbo;
    private bool accept;
    private bool cancel;
    private bool hittable;
    private bool moveHorizontalLock;
    private bool moveVerticalLock;
    private int shootLeftTimer;
    private int shootRightTimer;
    private int shootCooldown;
    private int weaponCooldown;
    private int weaponLevel;
    private bool acceptLock;
    //[SerializeField] private ;
    private Vector2 playerVelocity;
    private Vector3 cameraWorldMax, cameraWorldMin;

    private void PlayerControls() // Player controls code
    {
        if (moveLeft == true)
        {
            playerVelocity.x = -1;
            moveLeft = false;
            moveHorizontalLock = false;
        }
        else if (moveRight == true)
        {
            playerVelocity.x = 1;
            moveRight = false;
            moveHorizontalLock = false;
        }
        if (moveUp == true)
        {
            playerVelocity.y = 1;
            moveUp = false;
            moveVerticalLock = false;
        }
        else if (moveDown == true)
        {
            playerVelocity.y = -1;
            moveDown = false;
            moveVerticalLock = false;
        }
        if (shootLeft == true && shootRight == true)
        {
            if (shootLeftTimer > shootRightTimer)
            {
                ShootRightActivate();
            }
            else if (shootLeftTimer < shootRightTimer)
            {
                ShootLeftActivate();
            }
            else if (shootLeftTimer == shootRightTimer)
            {
                Debug.Log("gj you hit both shoot left and right on the same frame now go away"); // TODO: Decide what to do here
            }
            shootLeftTimer = shootLeftTimer + 1;
            shootRightTimer = shootRightTimer + 1;
            shootLeft = false;
            shootRight = false;
        }
        else if (shootLeft == true && shootRight == false)
        {
            shootLeftTimer = shootLeftTimer + 1;
            shootRightTimer = 0;
            ShootLeftActivate();
            shootLeft = false;
        }
        else if (shootLeft == false && shootRight == true)
        {
            shootRightTimer = shootRightTimer + 1;
            shootLeftTimer = 0;
            ShootRightActivate();
            shootRight = false;
        }
        else if (shootLeft == false && shootRight == false)
        {
            shootLeftTimer = 0;
            shootRightTimer = 0;
        }
        else
        {
            Debug.Log("how did we arrive here?");
        }
        if (accept == true)
        {
            accept = false;
            acceptLock = false;
        }
        else if (cancel == true)
        {
            cancel = false;
            acceptLock = false;
        }
        if (turbo == true)
        {
            turboMultiplier = turboLevel;
            turbo = false;
            TurboActivate();
        }
        if (bomb == true)
        {
            if (bombStock > 0 && bombTimer == 0)
            {
                BombActivate();
            }
            bomb = false;
        }
        if (pause == true)
        {
            //TODO call pause scene from here
            pause = false;
            SceneManager.LoadScene("Pause", LoadSceneMode.Single);
            Time.timeScale = 0;
        }
    }

    private void InputHandler() // Parsing controls
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                if (moveHorizontalLock == false)
                {
                    moveLeft = true;
                    moveHorizontalLock = true;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (moveHorizontalLock == false)
                {
                    moveRight = true;
                    moveHorizontalLock = true;
                }
            }
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (moveVerticalLock == false)
                {
                    moveDown = true;
                    moveVerticalLock = true;
                }
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (moveVerticalLock == false)
                {
                    moveUp = true;
                    moveVerticalLock = true;
                }
            }
        }
        if (Input.GetAxisRaw("ShootLeft") > 0)
        {
            shootLeft = true;
         }
        if (Input.GetAxisRaw("ShootRight") > 0)
        {
            shootRight = true;
        }
        if (Input.GetAxisRaw("Accept") != 0)
        {
            if (Input.GetAxisRaw("Accept") < 0)
            {
                if (acceptLock == false)
                {
                    cancel = true;
                    acceptLock = true;
                }
            }
            else if (Input.GetAxisRaw("Accept") > 0)
            {
                if (acceptLock == false)
                {
                    accept = true;
                    acceptLock = true;
                }
            }
        }
        if (Input.GetAxisRaw("Turbo") > 0)
        {
            turbo = true;
        }
        if (Input.GetAxisRaw("Bomb") > 0)
        {
            bomb = true;
        }
        if (Input.GetAxisRaw("Pause") > 0)
        {
            pause = true;
        }
    }

    private void PlayerMovement() // Actually handling player's movement
    {
        playerVelocity.Normalize();
        //rb2d.velocity = playerVelocity * speed * turboMultiplier;
        cameraWorldMax = mainCamera.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 10.00f));
        cameraWorldMin = mainCamera.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 10.00f));
        if (rb2d.transform.position.x <= cameraWorldMin.x)
        {
            playerVelocity = new Vector2(Mathf.Clamp(playerVelocity.x, 0f, 1f), playerVelocity.y);
        }
        if (rb2d.transform.position.x >= cameraWorldMax.x)
        {
            playerVelocity = new Vector2(Mathf.Clamp(playerVelocity.x, -1f, 0f), playerVelocity.y);
        }
        if (rb2d.transform.position.y <= cameraWorldMin.y)
        {
            playerVelocity = new Vector2(playerVelocity.x, Mathf.Clamp(playerVelocity.y, 0f, 1f));
        }
        if (rb2d.transform.position.y >= cameraWorldMax.y)
        {
            playerVelocity = new Vector2(playerVelocity.x, Mathf.Clamp(playerVelocity.y, -1f, 0f));
        }
        rb2d.velocity = playerVelocity * speed * turboMultiplier;
        playerVelocity = Vector2.zero;
        turboMultiplier = 1;
    }

    private void BombActivate() // bombing function
    {
        if (bombTimer == 0)
        {
            bombStock = bombStock - 1;
            bombTimer = 300;
            hittable = false;
        }
    }

    private void ShootLeftActivate() // shooting function
    {
        //TODO
        playerSpriteRenderer.flipX = true;
        if (shootCooldown <= 0)
        {
            ShootScript.ShootLeft(weaponLevel);
            shootCooldown = weaponCooldown - (1 * weaponLevel);
        }
    }

    private void ShootRightActivate()
    {
        // TODO
        playerSpriteRenderer.flipX = false;
        if (shootCooldown <= 0)
        {
            ShootScript.ShootRight(weaponLevel);
            shootCooldown = weaponCooldown - (1 * weaponLevel);
        }
    }

    private void TurboActivate() // turbo function
    {
        if (turboCooldownTimer == 0)
        {
            hittable = false;
            turboTimer = 180 + (120 * turboLevel);
            turboCooldownTimer = 180 - (20 * turboLevel);
        }
        turboMultiplier = turboLevel;
    }

    private void OnTriggerEnter2D(Collider2D collision) // TODO remaining collision types
    {
        if (collision.gameObject.CompareTag("Enemy") && hittable == true)
        {
            hittable = false;
            hitInvulTimer = 180;
            if (shield == true)
            {
                shield = false;
                shieldRecharge = 600 + (300 * shieldRechargePenalty);
                shieldRechargePenalty = shieldRechargePenalty + 1;
                //Debug.Log("shield lost");

            }
            else if (hp > 0)
            {
                bombStock = bombStock + 1;
                //hp = hp - 1;
                Debug.Log(hp);
                if ( hp <= 0 )
                {
                    //Debug.Log("Die.");
                    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                }
            }
            else
            { Debug.Log("Problem with enemy collision detection help");  }
        }
        if (collision.gameObject.CompareTag("EnemyProjectile") && hittable == true)
        {
            hittable = false;
            hitInvulTimer = 180;
            if (shield == true)
            {
                shield = false;
                shieldRecharge = 600 + (300 * shieldRechargePenalty);
                shieldRechargePenalty = shieldRechargePenalty + 1;
                //Debug.Log("shield lost");

            }
            else if (hp > 0)
            {
                bombStock = bombStock + 1;
                hp = hp - 1;
                //Debug.Log(hp);
                if (hp <= 0)
                {
                    //Debug.Log("Die.");
                    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                    // player death function
                }
            }
            else
            { Debug.Log("Problem with enemy collision detection help"); }
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            coins = coins + 1;
            // disable coin object that was collected
        }
    }

    private void CooldownTimers()
    {
        if (bombTimer > 0)
        {
            bombTimer = bombTimer - 1;
        }
        if (turboTimer > 0)
        {
            turboTimer = turboTimer - 1;
        }
        if (turbo == false && turboCooldownTimer > 0)
        {
            turboCooldownTimer = turboCooldownTimer - 1;
        }
        if (hitInvulTimer > 0)
        {
            hitInvulTimer = hitInvulTimer - 1;
        }
        if (shootCooldown > 0)
        {
            shootCooldown = shootCooldown - 1;
        }
        if (shieldRecharge > 0)
        {
            shieldRecharge = shieldRecharge - 1;
        }
    }

    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.useFullKinematicContacts = true;
        ShootScript = gameObject.GetComponent<PlayerShoot>();
        mainCamera = Camera.main;
        score = 0;
        scoreMultiplier = 1.0f;
        coins = 0;
        speed = 8;
        bombStock = 2;
        hp = 5;
        shieldRecharge = 0;
        shieldRechargePenalty = 0;
        turboMultiplier = 1;
        turboLevel = 3;
        weaponLevel = 1;
        hittable = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        InputHandler();
    }

    // Update is called once per frame
    void Update()
    {
        CooldownTimers();
        PlayerControls();
        PlayerMovement();
        if (hittable == false && bombTimer == 0 && turboTimer == 0 && hitInvulTimer == 0)
        {
            hittable = true;
        }
        if (shield == false && shieldRecharge <= 0)
        {
            shield = true;
        }
    }

    private void LateUpdate()
    {

    }
}
