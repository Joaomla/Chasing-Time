using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : Activation
{

    public override void Activate()
    {
        //throw new System.NotImplementedException();
        GetComponent<Door>().isOpening = true;
        GetComponent<Door>().isClosing = false;
    }

    public override void Deactivate()
    {
        //throw new System.NotImplementedException();
        GetComponent<Door>().isClosing = true;
        GetComponent<Door>().isOpening = false;
    }
}
