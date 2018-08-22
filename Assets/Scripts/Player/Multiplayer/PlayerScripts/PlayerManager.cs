
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

    // Explosion death prefab.
    public GameObject explosion;
    public Vector2 explosionPosition;

    // Audio
    private AudioSource source;
    public AudioClip death;

    // Is the player dead or alive?
    public bool dead = false;

    // Variables to keep track of.
    public Stats stats;
    [SyncVar]
    public int lives = 0;
    public int index = 0;
    [SyncVar]
    public int deaths = 0;
    [SyncVar]
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

        // Adding name for stats.
        stats.name = gameObject.name;

        yield return new WaitForSeconds(0.2f);

        // Find the index of the player (The LevelManager and CameraScript have lists where order is the same.)
        index = cameraScript.players.IndexOf(this.gameObject);

        // If the player is dead, remove them from the camera script list.
        Observable.EveryUpdate()
        .Where(_ => lives <= 0 || levelManager.players.Count == 2)
        .Subscribe(_ =>
        {
            cameraScript.players.RemoveAt(index);
            levelManager.syncPlayers.RemoveAt(index);
            if(!isServer)
                CmdAdd(stats);
            else
                RpcAdd(stats);

            Destroy(gameObject);

        }).AddTo(this);

        // Set spawn position depending on which player you are.
        spawnPosition = levelManager.respawns[(index == 0) ? 0 : 1];

    }


	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TAG_KILLZONE)
        {
            explosionPosition = transform.position;

            CmdSetStats();
            CmdDeath();

            // Explode in certain directions.
            switch(collision.gameObject.name)
            {
                case "KillZoneLeft":
                    if(!isServer)
                        CmdExplode(Quaternion.Euler(0,0,-90));
                    else
                        RpcExplode(Quaternion.Euler(0,0,-90), explosionPosition);
                    break;

                case "KillZoneBottom":
                    if(!isServer)
                        CmdExplode(Quaternion.identity);
                    else
                        RpcExplode(Quaternion.identity, explosionPosition);
                    break;

                case "KillZoneRight":
                    if(!isServer)
                        CmdExplode(Quaternion.Euler(0,0,-270));
                    else
                        RpcExplode(Quaternion.Euler(0,0,-270), explosionPosition);
                    break;

                case "KillZoneTop":
                    if(!isServer)
                        CmdExplode(Quaternion.Euler(0,0,-180));
                    else
                        RpcExplode(Quaternion.Euler(0,0,-180), explosionPosition);
                    break;
            }

            // For spawning on platform.
            rb.velocity = new Vector2(0,0);
            source.PlayOneShot(death);
            transform.position = spawnPosition.position;
            playerMovement.enabled = false;
            CmdDisable();

            // Figure out who to give the deaths to.
            if(index == 0)
            {
                GameManager.instance.p1Deaths = stats.deaths;
            }

            if(index == 1)
            {
                GameManager.instance.p2Deaths = stats.deaths;
            }

            // Disable controls.
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


    // -------------------------------------------------------- Networking stuff --------------------------------------------

    // Dead check (For platforms)
    public void SetDeath(bool death)
    {
        dead = death;

        // If you're alive, enable everything.
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

    [Command]
    public void CmdAdd(Stats stat)
    {
        RpcAdd(stat);
    }

    [ClientRpc]
    public void RpcAdd(Stats stat)
    {
        GameManager.instance.placeList.Add(stat);
    }

    [Command]
    public void CmdDeath()
    {
        dead = true;
        RpcDeath();
    }

    [ClientRpc]
    public void RpcDeath()
    {
        dead = true;
    }

    [Command]
    public void CmdSetStats()
    {
        RpcSetStats();
    }

    [ClientRpc]
    public void RpcSetStats()
    {
        if(playerHealth.koPlayer != null)
        {
            // Award kill to player.
            playerHealth.koPlayer.GetComponent<PlayerManager>().stats.kills++;
            
        }
        // Subtract lives, add to deaths.
        playerHealth.health = 0;
        --lives;
        ++stats.deaths;
    }

    [Command]
    public void CmdDisable()
    {
        if(dogControls != null)
        {
            dogControls.enabled = false;
        }

        if(catControls != null)
        {
            catControls.enabled = false;
        }
    }



    [Command]
    public void CmdExplode(Quaternion qt)
    {
        explosionPosition = transform.position;
        RpcExplode(qt, explosionPosition);
    }

    [ClientRpc]
    public void RpcExplode(Quaternion qt, Vector3 pos)
    {
        explosionPosition = transform.position;
        Instantiate(explosion, pos, qt);
    }

    
}

