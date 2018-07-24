using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using UniRx;

public class PlayerManager : NetworkBehaviour 
{
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
    private PlayerMovement playerMovement;

    // For different characters.
    private DogControls dogControls;
    private CatControls catControls;

    // For the camera script to keep track of the player.
    private CameraScript cameraScript;
    private LevelManager levelManager;

    // To subtract lives.
    private PlayerHealth playerHealth;

    // Audio
    private AudioSource source;
    public AudioClip death;

    // Is the player dead or alive?
    public bool dead = false;

    // Variables to keep track of.
    [SyncVar]
    public int lives = 0;
    public int index = 0;
    public int deaths = 0;
    public int kills = 0;

    // --------------------------------------------------------------------------------------------------------- //

    IEnumerator Start()
    {
        cameraScript = FindObjectOfType<CameraScript>();
        levelManager = FindObjectOfType<LevelManager>();
        playerHealth = GetComponent<PlayerHealth>(); 
        playerMovement = GetComponent<PlayerMovement>(); 
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();


        // Differentiate between characters.
        if(gameObject.name == TAG_DOG)
            dogControls = GetComponent<DogControls>();

        if(gameObject.name == TAG_CAT)
            catControls = GetComponent<CatControls>();

        // Find the index of the player (The LevelManager and CameraScript have lists where order is the same.)
        
        // Gamemode is KO Fest.
        if(GameManager.instance.gameModeNumber == 0)
        {
            lives = GameManager.instance.lives;
        }

        // Gamemode is timed - Lives are unlimited.
        if(GameManager.instance.gameModeNumber == 1)
        {
            lives = 9999;
        }

        yield return new WaitForSeconds(0.2f);
        index = cameraScript.players.IndexOf(this.gameObject);

        // If the player is dead, remove them from the camera script list.
        Observable.EveryUpdate()
        .Where(_ => lives <= 0)
        .Subscribe(_ =>
        {
            cameraScript.players.RemoveAt(index);
            levelManager.syncPlayers.RemoveAt(index);
            Destroy(gameObject);
        }).AddTo(this);

        spawnPosition = levelManager.respawns[(index == 0) ? 0 : 1];




        if(spawnPosition == null)
        {
            Debug.LogError("no spawn!");
        }
    }


	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            playerHealth.health = 0;
            if(playerHealth.koPlayer != null)
            {
                // Award kill to player.
                playerHealth.koPlayer.GetComponent<PlayerManager>().kills++;
                
                if(playerHealth.koPlayer.GetComponent<PlayerManager>().index == 0)
                    GameManager.instance.p1Kills = playerHealth.koPlayer.GetComponent<PlayerManager>().kills;
                else
                    GameManager.instance.p2Kills = playerHealth.koPlayer.GetComponent<PlayerManager>().kills;
            }
        
            // Subtract lives, add to deaths.
            --lives;
            ++deaths;
            dead = true;

            // For spawning on platform.
            rb.velocity = new Vector2(0,0);
            source.PlayOneShot(death);
            transform.position = spawnPosition.position;
            playerMovement.enabled = false;

            // Figure out who to give the deaths to.
            if(index == 0)
            {
                GameManager.instance.p1Deaths = deaths;
            }

            if(index == 1)
            {
                GameManager.instance.p2Deaths = deaths;
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
        if(!dead)
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

    // Dead check (For platforms)
    public void SetDeath(bool death)
    {
        dead = death;
    }

    
}

