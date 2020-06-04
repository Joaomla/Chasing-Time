using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Door door;

    //[SerializeField] bool somethingInside;


    List<Collider2D> triggerList = new List<Collider2D>();
    [SerializeField] int objectsInside;




    private void Update()
    {
        if (triggerList.Count > 0)
        {
            door.GetComponent<Door>().isOpening = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            door.GetComponent<Door>().isClosing = true;
            GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInside++;
        if (!triggerList.Contains(collision)){
            triggerList.Add(collision);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsInside--;
        if (triggerList.Contains(collision))
        {
            triggerList.Remove(collision);
        }
    }
}
