using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour 
{
	private string TAG_KILLZONE = "KillZone";
	private string TAG_PLATFORM = "Platform";

	public Transform spawnPosition;
    
    public Animator anim;

    private Rigidbody2D rb;

    private PlayerMovement playerMovement;
    private DogControls dogControls;
    private CatControls catControls;

    private CameraScript cameraScript;
    private PlayerHealth playerHealth;

    private AudioSource source;
    public AudioClip death;

    public bool dead = false;



    public LevelManager levelManager;
    public int lives = 0;
    public int index = 0;
    public int deaths = 0;
    public int kills = 0;
    public int time = 0;

    void Start()
    {
        cameraScript = FindObjectOfType<CameraScript>();
        levelManager = FindObjectOfType<LevelManager>();
        playerHealth = GetComponent<PlayerHealth>(); 
        playerMovement = GetComponent<PlayerMovement>(); 
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        if(gameObject.name == "DogFighter")
            dogControls = GetComponent<DogControls>();

        if(gameObject.name == "CatFighter")
            catControls = GetComponent<CatControls>();

        index = levelManager.players.IndexOf(this.gameObject);
        
        if(GameManager.instance.gameModeNumber == 0)
        {
            lives = GameManager.instance.lives;
        }

        if(GameManager.instance.gameModeNumber == 1)
        {
            lives = 9999;
        }

        

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
                playerHealth.koPlayer.GetComponent<PlayerManager>().kills++;
                
                if(playerHealth.koPlayer.GetComponent<PlayerManager>().index == 0)
                    GameManager.instance.p1Kills = playerHealth.koPlayer.GetComponent<PlayerManager>().kills;
                else
                    GameManager.instance.p2Kills = playerHealth.koPlayer.GetComponent<PlayerManager>().kills;
            }
            --lives;
            ++deaths;
            dead = true;
            rb.velocity = new Vector2(0,0);
            source.PlayOneShot(death);
            transform.position = spawnPosition.position;
            playerMovement.enabled = false;

            if(index == 0)
            {
                GameManager.instance.p1Deaths = deaths;
            }

            if(index == 1)
            {
                GameManager.instance.p2Deaths = deaths;
            }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        ++time;
        if(collision.gameObject.tag == TAG_PLATFORM && dead && time == 1)
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }


    private void Update() 
    {
        if(!dead)
        {
            time = 0;
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

    public void SetDeath(bool death)
    {
        dead = death;
    }

    
}

