using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSpecific : MonoBehaviour
{
    [SerializeField] Door door;
    [SerializeField] bool isTriggered;
    [SerializeField] GameObject myObject;

    Color originalColor;


    //List<Collider2D> triggerList = new List<Collider2D>();
    //[SerializeField] int objectsInside;

    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (isTriggered)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            door.GetComponent<Door>().isOpening = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = originalColor;
            door.GetComponent<Door>().isClosing = true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == myObject)
            isTriggered = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == myObject)
        {
            isTriggered = false;
        }
    }
}
