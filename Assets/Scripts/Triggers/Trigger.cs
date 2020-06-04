using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] List<GameObject> ActivateWhenActive;
    [SerializeField] List<GameObject> DeactivateWhenActive;
    [SerializeField] bool canDeactivate = false;

    public bool Triggered { get; set; }

    bool canTrigger = true;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsOpen", false);
        Triggered = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("here");
            if (canTrigger)
            {
                if (!Triggered)
                {
                    Triggered = true;

                    foreach (GameObject go in ActivateWhenActive)
                    {
                        go.GetComponent<Activation>().Activate();
                    }

                    foreach (GameObject go in DeactivateWhenActive)
                    {
                        go.GetComponent<Activation>().Deactivate();
                    }

                    animator.SetBool("IsOpen", true);
                }
                else if (canDeactivate)
                {
                    Triggered = false;

                    foreach (GameObject go in ActivateWhenActive)
                    {
                        go.GetComponent<Activation>().Deactivate();
                    }

                    foreach (GameObject go in DeactivateWhenActive)
                    {
                        go.GetComponent<Activation>().Activate();
                    }

                    animator.SetBool("IsOpen", false);
                }
                
            }
        }

        canTrigger = false;
    }

    private void OnTriggerStay2D( Collider2D collision )
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player")
            canTrigger = true;
    }

}
