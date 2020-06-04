using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject startPos;
    
    public bool onTopOfBridge = false;
    TimeController timeController;
    bool isReversing;

    private void Start()
    {
        timeController = FindObjectOfType<TimeController>();
        player = FindObjectOfType<Player>();
    }


    private void Update()
    {
        isReversing = timeController.GetReversing();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (!onTopOfBridge && !isReversing && collision.gameObject.name == "Player")
        {
            //Destroy(collision.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        // Resets box position if it gets wet (the player is pushing)
        if (!isReversing && collision.gameObject.tag == "Barrel")
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.transform.position = collision.GetComponent<Box>().OriginalPos;
            collision.GetComponent<Box>().StopPushing();
        }
               
    }
}
