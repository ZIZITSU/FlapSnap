using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class BirdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Rigidbody2D myRigidbody;

    private LogicManager logic;
    public float flapStrength;
    public bool Alive = true;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GameObject logicObject =
            GameObject.FindGameObjectWithTag("LogicTag");
        logic = logicObject.GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame && Alive)
        {
            Debug.Log("Space detected");

            myRigidbody.linearVelocity = new Vector2(
                myRigidbody.linearVelocity.x,
                flapStrength
            );
        }

        if(transform.position.y > 10 || transform.position.y < -10)
        {
            logic.gameOver();
            Alive = false;
        }


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        Alive = false;
    }
}
