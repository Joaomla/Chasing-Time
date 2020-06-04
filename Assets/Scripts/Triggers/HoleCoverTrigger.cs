using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCoverTrigger : Activation
{
    [SerializeField] Hole hole;
    Collider2D colliderB;
    [SerializeField] Renderer render;
    

    // The floor explodes
    public override void Activate()
    {
        //throw new System.NotImplementedException();
        render.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        hole.GetComponent<Hole>().onTopOfBridge = false;
    }

    // The floor magically appears again
    public override void Deactivate()
    {
        //throw new System.NotImplementedException();
        render.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
    
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.gameObject.name == "Player")
            hole.GetComponent<Hole>().onTopOfBridge = true;
    }

    
    private void OnTriggerStay2D( Collider2D collision )
    {
        if (collision.gameObject.name == "Player")
        hole.GetComponent<Hole>().onTopOfBridge = true;
    }
    

    private void OnTriggerExit2D( Collider2D collision )
    {
        if (collision.gameObject.name == "Player")
            hole.GetComponent<Hole>().onTopOfBridge = false;
    }

}
