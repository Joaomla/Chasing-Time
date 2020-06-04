using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    //bool IsInFrontOfDoor = false;
    public bool isOpening = false;
    public bool isClosing = false; 
    //[SerializeField] GameObject door;

    [Header ("DoorRoutes")]
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    [Header ("DoorSpeeds")]
    [SerializeField] float openingMoveSpeed = 3f;
    [SerializeField] float closingMoveSpeed = 8f;
    //[SerializeField] DoorTrigger button;


     
    
    private void Update()
    {
        if (isOpening)
        {
            OpenDoor();
        }
        if (isClosing)
        {
            CloseDoor();
        }
    }


    public bool GetDoorState()
    {
        return isOpening;

    }

    private void OpenDoor()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPoint.transform.position, openingMoveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, endPoint.transform.position) < 0.01f)
        {
            isOpening = false;
        }
        //Debug.Log(isOpening);
    }

    private void CloseDoor()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPoint.transform.position, closingMoveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, startPoint.transform.position) < 0.01f)
        {
            isClosing = false;
        }
    }

}
