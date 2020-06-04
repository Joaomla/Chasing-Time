using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bridge : MonoBehaviour
{

    Collider2D colliderB;
    [SerializeField] Hole hole;

    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("animation should run");
            hole.GetComponent<Hole>().onTopOfBridge = true;
            animator.SetBool("bridgeShake", true);
            colliderB = GetComponent<Collider2D>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            hole.GetComponent<Hole>().onTopOfBridge = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            hole.GetComponent<Hole>().onTopOfBridge = false;
            colliderB.enabled = false;
            animator.SetBool("bridgeShake", false);
            animator.SetBool("bridgeFall", true);
            Destroy(gameObject, 0.7f);
            
        }
    }

}
