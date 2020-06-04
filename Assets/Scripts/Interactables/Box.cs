using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private bool isPushable = false;
    [SerializeField] float angleRange = 30; // maximum angle which can make the player push/pull
    private Player player;

    public Vector3 OriginalPos { get; set;  }


    void Start()
    {
        player = FindObjectOfType<Player>();

        OriginalPos = new Vector3(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (isPushable)
        {
            
            gameObject.transform.parent = player.transform;

            if (Input.GetKeyUp(KeyCode.E))
            {
                player.gameObject.GetComponent<Player>().objHolding.Remove(gameObject);
                isPushable = false;
            }

        }
        else
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if (player.gameObject.GetComponent<Player>().objHolding.Count > 0)
            return;

        if (collision.gameObject.name == "Player")
            pushRequest(collision);
    }

    private void OnCollisionStay2D( Collision2D collision )
    {
        if (player.gameObject.GetComponent<Player>().objHolding.Count > 0)
            return;

        if (collision.gameObject.name == "Player")
            pushRequest(collision);
    }

    public void StopPushing()
    {
        isPushable = false;
    }


    private void pushRequest(Collision2D collision)
    {
        
        // the box of this class
        GameObject box = this.gameObject;

        // direction of the player
        Vector2 playerDirection = collision.gameObject.GetComponent<Player>().DirectionFacing;

        // Position of the player
        Vector3 playerPos3d = collision.gameObject.transform.position;
        Vector2 playerPos = new Vector2(playerPos3d.x, playerPos3d.y);

        // Direction of player to box
        Vector2 direction2box = (new Vector2(box.transform.position.x, box.transform.position.y)) - playerPos;

        // angle from player to box
        float angle2box = Vector2.Angle(playerDirection, direction2box);

        if (Input.GetKey(KeyCode.E))
        {
            if (angle2box < angleRange)
            {
                player.gameObject.GetComponent<Player>().objHolding.Add(box);
                isPushable = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            player.gameObject.GetComponent<Player>().objHolding.Remove(box);
            isPushable = false;
        }
    }
    
}
