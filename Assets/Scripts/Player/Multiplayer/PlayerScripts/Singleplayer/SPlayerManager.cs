using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SPlayerManager : MonoBehaviour 
{
    // To indentify each player
    public int id;

    // Tag for objects
    private const string TAG_DOG = "DogFighter";
    private const string TAG_CAT = "CatFighter";
	private string TAG_KILLZONE = "KillZone";
	private string TAG_PLATFORM = "Platform";

    // Where the player spawns initially.
	public Transform spawnPosition;

    // Animation
    public Animator anim;

    // To control the velocity of the character.
    private Rigidbody2D rb;

    // To disable the movement of the character while dead.
    private SPlayerMovement playerMovement;

    // For different characters.
    private SDogControls dogControls;
    private SCatControls catControls;

    // For the camera script to keep track of the player.
    private SCameraScript cameraScript;
    public SLevelManager levelManager;

    // To subtract lives.
    private SPlayerHealth playerHealth;

    // Explosion death prefab.
    public GameObject explosion;

    // Audio
    private AudioSource source;
    public AudioClip death;

    // Is the player dead or alive?
    public bool dead = false;

    // Variables to keep track of.
    public int lives = 0;
    public int index = 0;
    public int deaths = 0;
    public int kills = 0;

    // --------------------------------------------------------------------------------------------------------- //

    void Start()
    {
        cameraScript = FindObjectOfType<SCameraScript>();
        levelManager = FindObjectOfType<SLevelManager>();
        playerHealth = GetComponent<SPlayerHealth>(); 
        playerMovement = GetComponent<SPlayerMovement>(); 
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        // Differentiate between characters.
        if(gameObject.name == TAG_DOG)
            dogControls = GetComponent<SDogControls>();

        if(gameObject.name == TAG_CAT)
            catControls = GetComponent<SCatControls>();

        // Find the index of the player (The LevelManager and CameraScript have lists where order is the same.)
        index = levelManager.players.IndexOf(this.gameObject);
        
        // Gamemode is KO Fest.
        if(SGameManager.instance.gameModeNumber == 0)
        {
            lives = SGameManager.instance.lives;
        }

        // Gamemode is timed - Lives are unlimited.
        if(SGameManager.instance.gameModeNumber == 1)
        {
            lives = 9999;
        }

        // If the player is dead, remove them from the camera script list.
        Observable.EveryUpdate()
        .Where(_ => lives == 0)
        .Subscribe(_ =>
        {
            cameraScript.players.RemoveAt(index);
            levelManager.players[index] = null;
            levelManager.players.RemoveAt(index);
            Destroy(gameObject);
        }).AddTo(this);
    }


	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            playerHealth.health = 0;
            if(playerHealth.koPlayer != null)
            {
                // Award kill to player.
                playerHealth.koPlayer.GetComponent<SPlayerManager>().kills++;
                
                if(playerHealth.koPlayer.GetComponent<SPlayerManager>().index == 0)
                    SGameManager.instance.p1Kills = playerHealth.koPlayer.GetComponent<SPlayerManager>().kills;
                else
                    SGameManager.instance.p2Kills = playerHealth.koPlayer.GetComponent<SPlayerManager>().kills;
            }
        
            // Subtract lives, add to deaths.
            --lives;
            ++deaths;
            dead = true;


            // Explode in certain directions.
            switch(collision.gameObject.name)
            {
                case "KillZoneLeft":
                    Instantiate(explosion, transform.position, Quaternion.Euler(0,0,-90));
                    break;

                case "KillZoneBottom":
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    break;

                case "KillZoneRight":
                    Instantiate(explosion, transform.position, Quaternion.Euler(0,0,-270));
                    break;

                case "KillZoneTop":
                    Instantiate(explosion, transform.position, Quaternion.Euler(0,0,-180));
                    break;
            }


            // For spawning on platform.
            rb.velocity = new Vector2(0,0);
            source.PlayOneShot(death);
            transform.position = spawnPosition.position;
            playerMovement.enabled = false;

            // Figure out who to give the deaths to.
            if(index == 0)
            {
                SGameManager.instance.p1Deaths = deaths;
            }

            if(index == 1)
            {
                SGameManager.instance.p2Deaths = deaths;
            }

            // Disable controls.
            if(dogControls != null)
            {
                dogControls.enabled = false;
            }

            if(catControls != null)
            {
                catControls.enabled = false;
            }
        }
    }

    // Stick the Character to the platform.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == TAG_PLATFORM && dead)
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }


    private void Update() 
    {
        // If you're alive, enable everything.

            
    }

    // Dead check (For platforms)
    public void SetDeath(bool death)
    {
        dead = death;

        if(!death)
        {
            transform.parent = null;
            playerMovement.enabled = true;
            if(dogControls != null)
            {
                dogControls.enabled = true;
            }
            if(catControls != null)
            {
                catControls.enabled = true;
            }
        }
    }



    
}

