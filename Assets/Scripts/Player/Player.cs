using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject startPos;
    [SerializeField] int moveSpeed = 10;
    public Rigidbody2D rb;

    private TimeController timeController;

    public Vector2 DirectionFacing { get; set; }
    public Vector2 movement;
    public bool IsMoving { get; set; }
    public bool IsColliding { get; set; }

    public Animator animator;

    public Collision2D TheCollision { get; set; }

    public List<GameObject> objHolding;


    void Start()
    {
        timeController = FindObjectOfType<TimeController>() as TimeController;
        // initialization of the direction
        DirectionFacing = new Vector2(0, 0);
        // The player is not moving
        IsMoving = false;
        // Player will start in desired position
        transform.position = startPos.transform.position;

    }


    void Update()
    {
        if (!GetComponent<Collider2D>().isTrigger) //if (!timeController.isReversing)
        {
            GetComponent<TrailRenderer>().emitting = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            Move();
        }


        GetAnimation();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

    }

    private void GetAnimation()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void Move()
    {

        // We still don't know if the player is changing position
        IsMoving = false;

        var deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
        //rb.position = new Vector2(newXPos, newYPos);

        // Updates direction the player is facing
        if (movement.x != 0 || movement.y != 0)
        {
            // The player is changing position after all
            IsMoving = true;

            // Updates the direction the player is facing
            UpdateDirection(movement.x, movement.y);
        }
    }

    private void UpdateDirection( float dX, float dY )
    {
        // velocity direction
        Vector2 velocityDir = new Vector2(dX, dY);

        // angle between Up and the velocity direction
        float movementAngle = Vector2.SignedAngle(Vector2.up, velocityDir);

        if (Mathf.Abs(movementAngle) <= 45) // facing north
        {
            DirectionFacing = new Vector2(0, 1);
        }
        else if (Mathf.Abs(movementAngle) >= 135) // facing south
        {
            DirectionFacing = new Vector2(0, -1);
        }
        else if (movementAngle < 0) // Facing East
        {
            DirectionFacing = new Vector2(1, 0);
        }
        else    // Facing West
        {
            DirectionFacing = new Vector2(-1, 0);
        }

        //Debug.Log(movementAngle);
        //Debug.Log(DirectionFacing);

        return;
    }

    private void OnCollisionStay2D( Collision2D collision )
    {
        IsColliding = true;
        TheCollision = collision;
    }

    private void OnCollisionExit2D( Collision2D collision )
    {
        IsColliding = false;
        TheCollision = null;
    }
}
