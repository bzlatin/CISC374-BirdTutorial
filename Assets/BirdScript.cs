using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, flapStrength);
        }
        if(transform.position.y > 17 || transform.position.y < -17) {
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
