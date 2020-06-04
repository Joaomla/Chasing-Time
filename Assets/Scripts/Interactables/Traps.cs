using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{


    //[SerializeField] float closingSpeed = 10f;


    [SerializeField] Door door;
    [SerializeField] Trigger button;
    public bool isAcivated = false;
    TimeController timeController;
    bool isReversing;



    private void Start()
    {
        timeController = FindObjectOfType<TimeController>();
    }


    private void Update()
    {
        isReversing = timeController.GetReversing();

        if (isAcivated)
        {
            door.GetComponent<Door>().isClosing = true;
            button.GetComponent<Trigger>().Triggered = false;
            button.GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name == "Player" && !isReversing)
        {
            isAcivated = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !isReversing)
        {
            isAcivated = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isAcivated = false;
        }
    }


}
