using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Player player;
    public ArrayList playerPos;
    public bool isReversing = false;

    private bool isReversingBefore = false;
    private bool isStuck = false;


    //
    [SerializeField] int keyframe = 5;  // records the data every keyframe-th time it passes in FixedUpdate
    private int frameCounter = 0;   // Count frames until the next keyframe
    // Counts frame until the "last" keyframe. This variable mantains the speed when reversing.
    private int reverseCounter = 0;

    // Interpolation variables
    private Vector3 currentPosition;
    private Vector3 previousPosition;

    // first cycle of the reversing
    private bool firstRun = false;
    //

    
    [SerializeField] Text timerLabel;
    [SerializeField] float timerLimit = 5f;

    private float time;
    private bool timerStart = false;
    private bool arriveAtLimit = false;

    TrailRenderer trail;

    SpriteRenderer sprite;

    // Direction to go when stuck in a wall
    Vector3 direction2Go;
    [SerializeField] float unstuckVelocity = 1;
    Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = new ArrayList();
        player = FindObjectOfType<Player>() as Player;
        trail = player.GetComponent<TrailRenderer>();
        sprite = player.GetComponent<SpriteRenderer>();
        isReversing = false;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isStuck = false;
        isReversingBefore = isReversing;

        // Check if it's stuck in the door
        isStuck = checkIfStuck();

        if (isStuck)
        {
            // Unstuck the player
            playerRb.AddForce(direction2Go * unstuckVelocity);
        }
        else if (Input.GetKey(KeyCode.Space) && !player.IsMoving)
        {
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            isReversing = true;
            trail.emitting = true;
            sprite.color = new Color32(255, 255, 255, 100);
        }
        else
        {
            player.GetComponent<BoxCollider2D>().isTrigger = false;
            isReversing = false;

            // next time there' a reversing, there will be a first reversing cycle
            firstRun = true;
            timerStart = false;
            
        }

        // Checks if the timer arrived to the limit
        if (time >= timerLimit)
        {
            time = timerLimit;
        }


        arriveAtLimit = false;
        if (time <= 0)
        {
            arriveAtLimit = true;
            time = 0;
        }

        var seconds = time % 60;
        var fraction = (time * 100) % 100;

        timerLabel.text = string.Format("{0:00} : {1:000}", seconds, fraction);

        //Debug.Log(timerLabel);
    }

    void FixedUpdate()
    {
        if (!isReversing)
        {
            if (player.GetComponent<Player>().IsMoving)
            {
                if (frameCounter < keyframe)
                {
                    frameCounter++;
                }
                else
                {
                    frameCounter = 0;
                    playerPos.Add(player.transform.position);
                }

                time += Time.deltaTime;
            }
        }
        else
        {
            if (!arriveAtLimit)
            {
                //The player will not collide with other shit
                //player.GetComponent<BoxCollider2D>().isTrigger = true;

                if (reverseCounter > 0)
                {
                    reverseCounter--;
                }
                else
                {
                    reverseCounter = keyframe;
                    RestorePos();
                }

                // we need to call the RestorePos() on the firstRun because
                // the reverseCounter will be zero still
                if (firstRun)
                {
                    firstRun = false;
                    RestorePos();
                }

                int lastElementOfList = (playerPos.Count == 1 ? 1 : 0);

                // reverseCounter = 0 -> interpolation = 0
                // reverseCounter = 1 -> interpolation = 1
                float interpolation = ((float)reverseCounter / (float)keyframe) * (1 - lastElementOfList);

                // When interpolation is 0, returns previousPosition
                // Otherwise it returns currentPosition
                player.transform.position = Vector3.Lerp(previousPosition, currentPosition, interpolation);

                time -= Time.deltaTime;
            }
        }
    }

    public bool GetReversing()
    {
        return isReversing;
    }

    void RestorePos()
    {
        // Last and second to last indices of the position array
        int lastIndex = playerPos.Count - 1;
        int secondToLastIndex = playerPos.Count - 2;

        // if the array is not too small
        if (secondToLastIndex >= 0)
        {
            // Set current and previous positions
            currentPosition = (Vector3)playerPos[lastIndex];
            previousPosition = (Vector3)playerPos[secondToLastIndex];

            // remove current position from the position array
            playerPos.RemoveAt(lastIndex);
        }
    }

    private bool checkIfStuck()
    {
        // if in the last frame it was reversing but not anymore
        if (!isReversing && isReversingBefore)
        {
            if (player.IsColliding)
            {
                Collision2D theCollision = player.TheCollision;

                // it's a door!
                if (theCollision.gameObject.GetComponent<Door>() != null)
                {
                    float doorAngle = theCollision.transform.rotation.z;

                    Vector3 direction2Collision = player.transform.position - theCollision.transform.position;

                    // direction to go
                    if (doorAngle > 45 && doorAngle < 135) // y
                    {   
                        direction2Go = new Vector3(0, Mathf.Sign(direction2Collision.y), 0);
                    }
                    else  // x
                    {
                        direction2Go = new Vector3(Mathf.Sign(direction2Collision.x), 0, 0);
                    }


                    return true;
                }
            }
        }

        return false;
    }

}
