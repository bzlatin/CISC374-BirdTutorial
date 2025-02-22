using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;


    SoundEffectsPlayer audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPlayer>();

    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        // Spawn bird at top of screen 
        float screenTop = Camera.main.orthographicSize - 1f; // Adjust to stay inside the camera
        transform.position = new Vector3(0, screenTop, 0); // Spawn at (0, Top of Screen)

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            audioManager.PlaySFX(audioManager.jump);
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, flapStrength);

        }
        if(transform.position.y > 18 || transform.position.y < -18) {
            logic.gameOver();
            birdIsAlive = false;

        }
        
    }

    private void OnCollisionEnter2D(Collision2D colliison) 
    {
        logic.gameOver();
        birdIsAlive = false;
    }
}
