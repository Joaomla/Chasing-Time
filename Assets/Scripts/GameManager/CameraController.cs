using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cameras = FindObjectsOfType<Camera>();

        if (collision.gameObject.name == "Player")
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }

            GetComponent<Camera>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var cameras = FindObjectsOfType<Camera>();

        if (collision.gameObject.name == "Player")
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }

            GetComponent<Camera>().enabled = true;
        }
    }





}
